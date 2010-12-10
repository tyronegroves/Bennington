using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.Routing.Content;
using Paragon.ContentTree.Routing.Mappers;
using Paragon.ContentTree.Routing.Routing.Helpers;

namespace Paragon.ContentTree.Routing.Tests.Routing.Helpers
{
	[TestClass]
	public class TreeNodeIdToUrlTests_GetUrlByTreeNodeId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_url_for_third_level_tree_node()
		{
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
				         				Id = "0",
										ParentTreeNodeId = "-1",
				         			}, 
				         		new TreeNode()
				         			{
				         				Id = "1",
										ParentTreeNodeId = "0",
				         			},
 								new TreeNode()
				         		{
				         			Id = "2",
									ParentTreeNodeId = "1",
				         		},
 								new TreeNode()
				         		{
				         			Id = "3",
									ParentTreeNodeId = "2",
				         		}, 
							}.AsQueryable());
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("0"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = "0",
										Id = "1",
				         				UrlSegment = "urlSegment1",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("1"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = "1",
										Id = "2",
				         				UrlSegment = "urlSegment2",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("2"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = "2",
										Id = "3",
				         				UrlSegment = "urlSegment3",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "1")))
				.Returns(new ContentTreeNode()
				{
					NodeId = "1",
					UrlSegment = "urlSegment1",
				});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "2")))
				.Returns(new ContentTreeNode()
				{
					NodeId = "2",
					UrlSegment = "urlSegment2",
				});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "3")))
				.Returns(new ContentTreeNode()
				{
					NodeId = "3",
					UrlSegment = "urlSegment3",
				});

			var url = mocker.Resolve<TreeNodeIdToUrl>().GetUrlByTreeNodeId("3");

			Assert.AreEqual("/urlSegment1/urlSegment2/urlSegment3", url);
		}

		[TestMethod]
		public void Returns_correct_url_for_second_level_tree_node()
		{
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
				         				Id = "0",
										ParentTreeNodeId = "-1",
				         			}, 
				         		new TreeNode()
				         			{
				         				Id = "1",
										ParentTreeNodeId = "0",
				         			},
 								new TreeNode()
				         		{
				         			Id = "2",
									ParentTreeNodeId = "1",
				         		}, 
							}.AsQueryable());
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("0"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = "0",
										Id = "1",
				         				UrlSegment = "urlSegment1",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("1"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = "1",
										Id = "2",
				         				UrlSegment = "urlSegment2",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "1")))
				.Returns(new ContentTreeNode()
				{
					NodeId = "1",
					UrlSegment = "urlSegment1",
				});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "2")))
				.Returns(new ContentTreeNode()
				{
					NodeId = "2",
					UrlSegment = "urlSegment2",
				});

			var url = mocker.Resolve<TreeNodeIdToUrl>().GetUrlByTreeNodeId("2");

			Assert.AreEqual("/urlSegment1/urlSegment2", url);
		}

		[TestMethod]
		public void Returns_correct_url_for_first_level_tree_node()
		{
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
				         				Id = "0",
										ParentTreeNodeId = "-1",
				         			}, 
				         		new TreeNode()
				         			{
				         				Id = "2",
										ParentTreeNodeId = "0",
				         			}, 
							}.AsQueryable());
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("0"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = "0",
										Id = "2",
				         				UrlSegment = "urlSegment",
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryToContentTreeNodeMapper>().Setup(
				a => a.CreateInstance(It.Is<TreeNodeSummary>(b => b.Id == "2")))
				.Returns(new ContentTreeNode()
				         	{
				         		NodeId = "2",
								UrlSegment = "urlSegment",
				         	});

			var url = mocker.Resolve<TreeNodeIdToUrl>().GetUrlByTreeNodeId("2");

			Assert.AreEqual("/urlSegment", url);
		}
	}
}