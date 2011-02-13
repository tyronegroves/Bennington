using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paragon.ContentTree.Contexts
{
	public interface IVersionContext
	{
		string GetCurrentVersionId();
	}

	public class VersionContext : IVersionContext
	{
		public const string Draft = "Draft";
		public const string Publish =  "Publish";

		public string GetCurrentVersionId()
		{
			try
			{
				if (HttpContext.Current.Request.RawUrl.StartsWith("/Manage")) return Draft;

				if (HttpContext.Current.Request.QueryString["Version"] == Draft) return Draft;
			}catch(Exception) {}

			return Publish;
		}
	}
}