using System;
using System.IO;
using System.Linq;
using Bennington.ContentTree.Helpers;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Data
{
	public interface IDataModelDataContext
	{
		IQueryable<TreeNode> TreeNodes { get; set; }
		void Create(TreeNode treeNode);
		void Delete(string id);
		void Update(TreeNode treeNode);
	}

	public class DataModelDataContext : IDataModelDataContext
	{
		private static readonly object _lockObject = "lock";

		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;
		private readonly IGetPathToDataDirectoryService getPathToDataDirectoryService;

		public DataModelDataContext(IXmlFileSerializationHelper xmlFileSerializationHelper,
									IGetPathToDataDirectoryService getPathToDataDirectoryService)
		{
			this.getPathToDataDirectoryService = getPathToDataDirectoryService;
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
		}

		public IQueryable<TreeNode> TreeNodes
		{
			get
			{
			    var path = GetXmlFilePath();
			    return xmlFileSerializationHelper.DeserializeListFromPath<TreeNode>(path).AsQueryable();
			}
			set { throw new NotImplementedException(); }
		}

		public void Create(TreeNode treeNode)
		{
			lock (_lockObject)
			{
				var list = xmlFileSerializationHelper.DeserializeListFromPath<TreeNode>(GetXmlFilePath());
				list.Add(treeNode);
				xmlFileSerializationHelper.SerializeListToPath(list, GetXmlFilePath());
			}
		}

		public void Delete(string id)
		{
			lock (_lockObject)
			{
				var list = xmlFileSerializationHelper.DeserializeListFromPath<TreeNode>(GetXmlFilePath());
				list.Remove(list.Where(a => a.Id == id).FirstOrDefault());
				xmlFileSerializationHelper.SerializeListToPath(list, GetXmlFilePath());
			}
		}

		public void Update(TreeNode treeNode)
		{
			lock(_lockObject)
			{
				var list = xmlFileSerializationHelper.DeserializeListFromPath<TreeNode>(GetXmlFilePath());
				list.Remove(list.Where(a => a.Id == treeNode.Id).FirstOrDefault());
				list.Add(treeNode);
				xmlFileSerializationHelper.SerializeListToPath(list, GetXmlFilePath());
			}
		}

		private string GetXmlFilePath()
		{
		    var path = getPathToDataDirectoryService.GetPathToDirectory();
			return path + Path.DirectorySeparatorChar + "TreeNodes.xml";
		}
	}
}