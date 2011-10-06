using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Bennington.Cms.PrincipalProvider.HttpModules
{
    public class LogoutHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(CheckRequestForLogoutQuerystring);
        }

        void CheckRequestForLogoutQuerystring(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            if (app.Request.QueryString.AllKeys.Contains("Logout"))
            {
                Logout();
                app.Response.Redirect("~/");
            }
        }

        private void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public void Dispose()
        {
        }
    }
}