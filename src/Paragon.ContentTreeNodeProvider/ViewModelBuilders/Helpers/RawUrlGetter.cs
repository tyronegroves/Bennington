using System.Web;

namespace Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders.Helpers
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
