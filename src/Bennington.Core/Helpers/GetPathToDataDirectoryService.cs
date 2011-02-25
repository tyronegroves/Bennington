using System.IO;
using System.Web;

namespace Bennington.Core.Helpers
{
	public interface IGetPathToDataDirectoryService
	{
		string GetPathToDirectory();
	}

	public class GetPathToDataDirectoryService : IGetPathToDataDirectoryService
	{
		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
		private const string BenningtonContentTreeDataFolderName = "BenningtonContentTreeData";

		public GetPathToDataDirectoryService(IApplicationSettingsValueGetter applicationSettingsValueGetter)
		{
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
		}

		public string GetPathToDirectory()
		{
			var overridePath = applicationSettingsValueGetter.GetValue("BenningtonDataPath");
			if (!string.IsNullOrEmpty(overridePath))
				return overridePath;

			var x = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
			if (x.IndexOf("DevServer", 0) > 0)
			{
				var z = Directory.GetParent(HttpContext.Current.Server.MapPath("/"));
				z = Directory.GetParent(z.Parent.FullName);
				return z.FullName + Path.DirectorySeparatorChar + "localWorkingFolder" + Path.DirectorySeparatorChar + BenningtonContentTreeDataFolderName;
			}

			return System.IO.Directory.GetParent(HttpContext.Current.Server.MapPath("/")).Parent.FullName + System.IO.Path.DirectorySeparatorChar + BenningtonContentTreeDataFolderName;
		}
	}
}