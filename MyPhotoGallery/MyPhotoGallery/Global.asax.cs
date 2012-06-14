using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MyPhotoGallery
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            Exception ex = Server.GetLastError();
            string redirectUrlString = "~/Error.aspx?";
            if (ex.InnerException != null)
            {
                redirectUrlString += "inner=" + ex.InnerException.Message.ToString().Replace(' ', '+') + "&";
            }
            redirectUrlString += "msg=" + ex.Message.ToString().Replace(' ', '+');
            Response.Redirect(redirectUrlString);
            Server.ClearError();
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
