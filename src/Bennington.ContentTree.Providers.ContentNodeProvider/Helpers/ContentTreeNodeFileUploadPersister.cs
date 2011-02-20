using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Helpers
{
	public interface IContentTreeNodeFileUploadPersister
	{
		void SaveFilesByTreeNodeIdAndAction(string treeNodeId, string action);
	}

	public class ContentTreeNodeFileUploadPersister : IContentTreeNodeFileUploadPersister
	{
		private readonly IGetPathToContentTreeNodeProviderFileUploads getPathToContentTreeNodeProviderFileUploads;

		public ContentTreeNodeFileUploadPersister(IGetPathToContentTreeNodeProviderFileUploads getPathToContentTreeNodeProviderFileUploads)
		{
			this.getPathToContentTreeNodeProviderFileUploads = getPathToContentTreeNodeProviderFileUploads;
		}

		public void SaveFilesByTreeNodeIdAndAction(string treeNodeId, string action)
		{
			if (HttpContext.Current.Request.Files.AllKeys.Where(a => a == "ContentTreeNodeInputModel_HeaderImage").Any())
			{
				var path = string.Format(@"{0}{1}\{2}\", 
									getPathToContentTreeNodeProviderFileUploads.GetPath(),
									treeNodeId, 
									action);
				Directory.CreateDirectory(path);
				HttpContext.Current.Request.Files["ContentTreeNodeInputModel_HeaderImage"].SaveAs(path + HttpContext.Current.Request.Files["ContentTreeNodeInputModel_HeaderImage"].FileName);
			}
		}
	}
}
