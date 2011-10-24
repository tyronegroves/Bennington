using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Bennington.ContentTree.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Mappers
{
	[TestClass]
	public class ContentTreeNodeToContentTreeNodeInputModelMapperTest
	{
		private AutoMoqer mocker;
		
		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Assert_configuration_is_valid()
		{
			var mapper = mocker.Resolve<ContentTreeNodeToContentTreeNodeInputModelMapper>();
			mapper.AssertConfigurationIsValid();
		}

		[TestMethod]
		public void CreateInstance_sets_Type_value_from_tree_node_type()
		{
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
				         				Id = "1",
										Type = "testType"
				         			}, 
							}.ToList());

			var mapper = mocker.Resolve<ContentTreeNodeToContentTreeNodeInputModelMapper>();
			var result = mapper.CreateInstance(new ContentTreeNode()
			                      	{
										TreeNodeId = "1",
			                      	});

			Assert.AreEqual("testType", result.Type);
		}
	}
}
