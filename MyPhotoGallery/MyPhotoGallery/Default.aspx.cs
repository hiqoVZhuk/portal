using System;
using System.IO;
using MyPhotoGallery.DataAccess;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Web.UI.HtmlControls;

namespace MyPhotoGallery
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void uploadButton_Click(object sender, EventArgs e)
        {
            var divtimespan = Master.FindControl("divtimespan") as HtmlGenericControl;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //get input data
            DateTime now = DateTime.Now;
            string title = tbTitle.Text;
            string fileName = FileUpload1.FileName;
            string ext = Path.GetExtension(fileName);
            byte[] file = FileUpload1.FileBytes;
            using (var input = new MemoryStream(file))
            {
                //watermarking image
                var output = new MemoryStream();
                AddWaterMark(input, now.ToString(), output);
                file = new byte[output.Length];
                output.Position = 0;
                BinaryReader br = new BinaryReader(output);
                file = br.ReadBytes((int)output.Length);
                br.Close();
            }

            //save image to disk and database
            string path = Server.MapPath("Images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName = Guid.NewGuid().GetHashCode().ToString("x") + ext;
            path = path + "\\" + fileName;
            try
            {
                var ge = new GalleryEntities();
                Photos photo = new Photos();
                photo.fileName = fileName;
                photo.postingDate = DateTime.Now;
                photo.title = title;
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(file);
                }
                ge.AddToPhotos(photo);
                ge.SaveChanges();
            }
            finally { }
            ListView_Photos.DataBind();
            sw.Stop();
            if (divtimespan != null)
                divtimespan.InnerText = string.Format("Elapsed time: {0}", sw.Elapsed);
        }

        public static void AddWaterMark(MemoryStream ms, string watermarkText, MemoryStream outputStream)
        {
            using (Image img = Image.FromStream(ms))
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == img.RawFormat.Guid)
                        ici = codec;
                }

                using (Graphics gr = Graphics.FromImage(img))
                {
                    Font font = new Font("Tahoma", 10);
                    SizeF sizef = gr.MeasureString(watermarkText, font, int.MaxValue);

                    gr.DrawString(watermarkText, font, Brushes.LightGray, new Point(0, 0));
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                }
                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
                img.Save(outputStream, ici, ep);
            }
        }
    }
}
