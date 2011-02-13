using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.Core.Helpers;

namespace Paragon.ContentTree.Data
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
		private const string PathToContentTreeXmlFileAppSettingsKey = "PathToContentTreeXmlFile";

		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;

		public DataModelDataContext(IApplicationSettingsValueGetter applicationSettingsValueGetter,
									IXmlFileSerializationHelper xmlFileSerializationHelper)
		{
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
		}

		public IQueryable<TreeNode> TreeNodes
		{
			get { return xmlFileSerializationHelper.DeserializeListFromPath<TreeNode>(applicationSettingsValueGetter.GetValue(PathToContentTreeXmlFileAppSettingsKey)).AsQueryable(); }
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
			return applicationSettingsValueGetter.GetValue(PathToContentTreeXmlFileAppSettingsKey);
		}
	}
}