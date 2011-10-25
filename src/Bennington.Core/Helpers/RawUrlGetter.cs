using System.Web;

namespace Bennington.Core.Helpers
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
		}
	}
}
