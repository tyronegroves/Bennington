using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Bennington.ContentTree.Helpers
{
    public interface IGetUrlOfFrontSideWebsite
    {
        string GetUrlOfFrontSide();
    }

    public class GetUrlOfFrontSideWebsite : IGetUrlOfFrontSideWebsite
    {
        public virtual string GetUrlOfFrontSide()
        {
            if (ConfigurationManager.AppSettings["UrlForFrontSideWebsite"] != null)
                return ConfigurationManager.AppSettings["UrlForFrontSideWebsite"];

            return string.Empty;
        }
    }
}