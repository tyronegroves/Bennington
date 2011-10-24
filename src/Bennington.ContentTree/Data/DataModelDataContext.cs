using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bennington.ContentTree.Denormalizers;
using Bennington.ContentTree.Helpers;
using Bennington.Core.Helpers;
using Bennington.Core.SisoDb;

namespace Bennington.ContentTree.Data
{
	public interface IDataModelDataContext
	{
        IList<TreeNode> TreeNodes { get; set; }
		void Create(TreeNode treeNode);
		void Delete(string id);
		void Update(TreeNode treeNode);
	}

    public class DataModelDataContext : DatabaseFactory, IDataModelDataContext
	{
		private static readonly object _lockObject = "lock";

		private readonly Func<IApplicationSettingsValueGetter> applicationSettingsValueGetter;
		private readonly Func<IXmlFileSerializationHelper> xmlFileSerializationHelper;
		private readonly Func<IGetPathToDataDirectoryService> getPathToDataDirectoryService;

        public DataModelDataContext(Func<IXmlFileSerializationHelper> xmlFileSerializationHelper,
                                    Func<IGetPathToDataDirectoryService> getPathToDataDirectoryService)
		{
			this.getPathToDataDirectoryService = getPathToDataDirectoryService;
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
		}

		public IList<TreeNode> TreeNodes
		{
			get
			{
			    IList<TreeNode> test;
                using (var unitOfWork = database.CreateUnitOfWork())
                {
                    test = unitOfWork.GetAll<TreeNode>().ToList();
                }

			    return test;
			}

			set { throw new NotImplementedException(); }
		}

		public void Create(TreeNode treeNode)
		{
			lock (_lockObject)
			{
				var list = xmlFileSerializationHelper.Invoke().DeserializeListFromPath<TreeNode>(GetXmlFilePath());
				list.Add(treeNode);
				xmlFileSerializationHelper.Invoke().SerializeListToPath(list, GetXmlFilePath());
			}
		}

		public void Delete(string id)
		{
			lock (_lockObject)
			{
				var list = xmlFileSerializationHelper.Invoke().DeserializeListFromPath<TreeNode>(GetXmlFilePath());
				list.Remove(list.Where(a => a.Id == id).FirstOrDefault());
				xmlFileSerializationHelper.Invoke().SerializeListToPath(list, GetXmlFilePath());
			}
		}

		public void Update(TreeNode treeNode)
		{
			lock(_lockObject)
			{
				var list = xmlFileSerializationHelper.Invoke().DeserializeListFromPath<TreeNode>(GetXmlFilePath());
				list.Remove(list.Where(a => a.Id == treeNode.Id).FirstOrDefault());
				list.Add(treeNode);
				xmlFileSerializationHelper.Invoke().SerializeListToPath(list, GetXmlFilePath());
			}
		}

		private string GetXmlFilePath()
		{
		    var path = getPathToDataDirectoryService.Invoke().GetPathToDirectory();
			return path + Path.DirectorySeparatorChar + "TreeNodes.xml";
		}
	}
}