using System;
using System.IO;
using System.Web;

namespace Bennington.Core.Helpers
{
    public interface IGetPathToDataDirectoryService
    {
        string GetPathToDirectory();
    }

    public interface IGetPathToWorkingDirectoryService
    {
        string GetPathToDirectory();
    }

    public class GetPathToWorkingDirectoryService : IGetPathToWorkingDirectoryService
    {
        private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;

        public GetPathToWorkingDirectoryService(IApplicationSettingsValueGetter applicationSettingsValueGetter)
        {
            this.applicationSettingsValueGetter = applicationSettingsValueGetter;
        }

        public string GetPathToDirectory()
        {
            var overridePath = applicationSettingsValueGetter.GetValue("Bennington.LocalWorkingFolder");
            if (!string.IsNullOrEmpty(overridePath))
                return overridePath;

            const string localWorkingFolderName = "localWorkingFolder";

            var x = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (x.IndexOf("DevServer", 0) > 0)
            {
                var rootPath = string.Empty;
                try
                {
                    rootPath = HttpContext.Current.Server.MapPath("/");
                }
                catch (Exception)
                {
                    rootPath = HttpContext.Current.Server.MapPath("/Manage");
                }

                var z = Directory.GetParent(rootPath).Parent.Parent.FullName;
                return z + Path.DirectorySeparatorChar + localWorkingFolderName + Path.DirectorySeparatorChar;
            }

            return System.IO.Directory.GetParent(HttpContext.Current.Server.MapPath("/")).Parent.FullName + System.IO.Path.DirectorySeparatorChar + localWorkingFolderName + Path.DirectorySeparatorChar;
        }        
    }

    public class GetPathToDataDirectoryService : IGetPathToDataDirectoryService
    {
        private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
        private readonly IGetPathToWorkingDirectoryService getPathToWorkingDirectoryService;
        private const string BenningtonContentTreeDataFolderName = "BenningtonData";

        public GetPathToDataDirectoryService(IApplicationSettingsValueGetter applicationSettingsValueGetter,
                                            IGetPathToWorkingDirectoryService getPathToWorkingDirectoryService)
        {
            this.getPathToWorkingDirectoryService = getPathToWorkingDirectoryService;
            this.applicationSettingsValueGetter = applicationSettingsValueGetter;
        }

        public string GetPathToDirectory()
        {
            var overridePath = applicationSettingsValueGetter.GetValue("Bennington.DataPath");
            if (!string.IsNullOrEmpty(overridePath))
                return overridePath;

            return getPathToWorkingDirectoryService.GetPathToDirectory() + Path.DirectorySeparatorChar + BenningtonContentTreeDataFolderName + Path.DirectorySeparatorChar;
        }
    }
}