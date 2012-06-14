using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyPhotoGallery.DataAccess;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace MyPhotoGallery
{
    public partial class PhotoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPostComment_Click(object sender, EventArgs e)
        {
            var divtimespan = Master.FindControl("divtimespan") as HtmlGenericControl;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string photoIdstring = Request["photoId"];
            int photoId = -1;
            if (!int.TryParse(photoIdstring, out photoId)) return;

            using (GalleryEntities ge = new GalleryEntities())
            {
                Comments c = new Comments();
                c.photoId = photoId;
                c.name = tbName.Text;
                c.text = tbComment.Text;
                ge.AddToComments(c);
                ge.SaveChanges();
            }

            ListView1.DataBind();

            sw.Stop();
            if (divtimespan != null)
                divtimespan.InnerText = string.Format("Elapsed time: {0}", sw.Elapsed);
        }
    }
}