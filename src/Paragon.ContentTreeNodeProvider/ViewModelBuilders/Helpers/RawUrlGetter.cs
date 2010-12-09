using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Paragon.ContentTreeNodeProvider.ViewModelBuilders.Helpers
{
	public interface IRawUrlGetter
	{
		string GetRawUrl();
	}

	public class RawUrlGetter : IRawUrlGetter
	{
		public string GetRawUrl()
		{
			return HttpContext.Current.Request.Url.LocalPath;
			// return HttpContext.Current.Request.RawUrl;
		}
	}
}
