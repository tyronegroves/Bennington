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
	public class TreeNodeSummaryContextToContentTreeRepositoryAdapterTests_GetChildNodesForNode
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void REturns_children_of_specified_node_from_ITreeNodeSummaryContext_mapped_through_ITreeNodeSummaryToContentTreeNodeMapper()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("1"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "a",
				         			}, 
				         		new TreeNodeSummary()
				         			{
				         				Id = "b",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "a")))
				.Returns(new ContentTreeNode()
				         	{
								NodeId = "a",
				         	});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "b")))
				.Returns(new ContentTreeNode()
				{
					NodeId = "b",
				});


			var result = mocker.Resolve<TreeNodeSummaryContextToContentTreeRepositoryAdapter>().GetChildNodesForNode(new ContentTreeNode()
			                                                                                            	{
			                                                                                            		NodeId = "1",
			                                                                                            	});

			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.Where(a => a.NodeId == "a").Count());
			Assert.AreEqual(1, result.Where(a => a.NodeId == "b").Count());
		}
	}
}
