using System;
using System.Web;

namespace Bennington.ContentTree.Contexts
{
	public interface IVersionContext
	{
		string GetCurrentVersionId();
	}

	public class VersionContext : IVersionContext
	{
		public const string Draft = "Draft";
		public const string Publish =  "Publish";
		public const string Manage = "Manage";

		public string GetCurrentVersionId()
		{
            if (HttpContext.Current == null) return Publish;
            if (HttpContext.Current.Request == null) return Publish;
            if (HttpContext.Current.Request.RawUrl == null) return Publish;

            if (HttpContext.Current.Request.RawUrl.StartsWith("/ContentTree")) return Manage;

            //if (HttpContext.Current.Request.RawUrl.StartsWith("/Manage", StringComparison.InvariantCultureIgnoreCase)) return Manage;

            if (HttpContext.Current.Request.QueryString["Version"] == Draft) return Draft;

			return Publish;
		}
	}
}