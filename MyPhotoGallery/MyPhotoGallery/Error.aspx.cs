using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyPhotoGallery
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = Request["msg"];
            string inner = Request["inner"];

            if (msg != null)
                msgdiv.InnerText = msg.Replace('+', ' ');
            if (inner != null)
                innerdiv.InnerText = inner.Replace('+', ' ');
        }
    }
}