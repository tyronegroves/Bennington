using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Models;
using Paragon.Pages.Adapters;
using Paragon.Pages.Content;
using Paragon.Pages.Mappers;

namespace Paragon.Pages.Tests.Adapters
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
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(TreeNodeSummaryContextToContentTreeRepositoryAdapter.RootNodeParentId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		rootNode,
							}.AsQueryable());

			var result = mocker.Resolve<TreeNodeSummaryContextToContentTreeRepositoryAdapter>().GetRootNode();
			
			Assert.AreEqual("0", result.NodeId);
		}
	}
}
