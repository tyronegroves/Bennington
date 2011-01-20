using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Routing.Adapters;
using Paragon.ContentTree.Routing.Content;
using Paragon.ContentTree.Routing.Mappers;

namespace Paragon.ContentTree.Routing.Tests.Adapters
{
	[TestClass]
	public class TreeNodeSummaryContextToContentTreeRepositoryAdapterTests_GetRootNode
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_node_with_null_parent_id()
		{
			var rootNode = new TreeNodeSummary()
			               	{
			               		Id = "0",
			               	};
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "0")))
				.Returns(new ContentTreeNode()
				         			{
				         				NodeId = "0",
				         			});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(TreeNodeSummaryContextToContentTreeRepositoryAdapter.RootNodeParentNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		rootNode,
							}.AsQueryable());

			var result = mocker.Resolve<TreeNodeSummaryContextToContentTreeRepositoryAdapter>().GetRootNode();
			
			Assert.AreEqual("0", result.NodeId);
		}

		[TestMethod]
		public void Returns_properly_setup_root_node_when_the_ITreeNodeSummaryContext_returns_empty_set()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(TreeNodeSummaryContextToContentTreeRepositoryAdapter.RootNodeParentNodeId))
				.Returns(new TreeNodeSummary[]{}.AsQueryable());

			var result = mocker.Resolve<TreeNodeSummaryContextToContentTreeRepositoryAdapter>().GetRootNode();

			Assert.AreEqual(Constants.RootNodeId, result.NodeId);
			Assert.AreEqual(Constants.RootNodeParentId, result.Parent.NodeId);
		}
	}
}
