using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Mappers
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
							}.AsQueryable());

			var mapper = mocker.Resolve<ContentTreeNodeToContentTreeNodeInputModelMapper>();
			var result = mapper.CreateInstance(new ContentTreeNode()
			                      	{
										TreeNodeId = "1",
			                      	});

			Assert.AreEqual("testType", result.Type);
		}
	}
}
