using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Helpers
{
	public interface IGetPathToContentTreeNodeProviderFileUploads
	{
		string GetPath();
	}

	public class GetPathToContentTreeNodeProviderFileUploads : IGetPathToContentTreeNodeProviderFileUploads
	{
		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
	    private readonly IGetPathToWorkingDirectoryService getPathToWorkingDirectoryService;

	    public GetPathToContentTreeNodeProviderFileUploads(IApplicationSettingsValueGetter applicationSettingsValueGetter,
                                                                IGetPathToWorkingDirectoryService getPathToWorkingDirectoryService)
	    {
	        this.getPathToWorkingDirectoryService = getPathToWorkingDirectoryService;
	        this.applicationSettingsValueGetter = applicationSettingsValueGetter;
	    }

	    public string GetPath()
	    {
	        var overridePath =
	            applicationSettingsValueGetter.GetValue(
	                "Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath");

            if (!string.IsNullOrEmpty(overridePath)) return overridePath;

	        return string.Format("{0}{1}{2}", getPathToWorkingDirectoryService.GetPathToDirectory(), "FileUploads", Path.DirectorySeparatorChar);
	    }
	}
}
