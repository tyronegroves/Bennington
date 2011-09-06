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
			try
			{
                if (HttpContext.Current.Request.RawUrl.StartsWith("/Manage", StringComparison.InvariantCultureIgnoreCase)) return Manage;

				if (HttpContext.Current.Request.QueryString["Version"] == Draft) return Draft;
			}catch(Exception) {}

			return Publish;
		}
	}
}