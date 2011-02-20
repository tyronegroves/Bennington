using System;
using System.Collections.Generic;
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

		public GetPathToContentTreeNodeProviderFileUploads(IApplicationSettingsValueGetter applicationSettingsValueGetter)
		{
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
		}

		public string GetPath()
		{
			return applicationSettingsValueGetter.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath");
		}
	}
}
