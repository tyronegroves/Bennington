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
			if (HttpContext.Current.Request.RawUrl.StartsWith("/Manage")) return Draft;

			return Publish;
		}
	}
}