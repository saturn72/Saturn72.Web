using System;
using System.Web;

namespace Saturn72.Web.UI
{
    public class MvcApplication : HttpApplication
    {
        //TODO: Move to Owin Startup class
        protected void Application_Error(object sender, EventArgs e)
        {
            //TODO: Complete on_Error functinality
            var exception = Server.GetLastError();
            throw exception;
        }
    }
}