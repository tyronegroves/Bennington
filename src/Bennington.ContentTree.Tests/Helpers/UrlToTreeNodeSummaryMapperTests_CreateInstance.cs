using System;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Tests.Helpers
{
	[TestClass]
	public class UrlToTreeNodeSummaryMapperTests_CreateInstance
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_null_when_passed_null()
		{
			var result = mocker.Resolve<UrlToTreeNodeSummaryMapper>().CreateInstance(null);

			Assert.IsNull(result);
		}

		[TestMethod]
		public void Returns_null_for_404()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(Constants.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = pageId.ToString(),
										UrlSegment = "testpage",
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = pageId.ToString(),
										UrlSegment = "testpage2",
				         			}
							});

			var result = mocker.Resolve<UrlToTreeNodeSummaryMapper>().CreateInstance("/asdf");

			Assert.IsNull(result);
		}

		[TestMethod]
		public void Returns_correct_tree_node_summary_when_passed_a_url_1_deep()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(Constants.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = pageId.ToString(),
										UrlSegment = "testpage",
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = pageId.ToString(),
										UrlSegment = "testpage2",
				         			}
							});

			var result = mocker.Resolve<UrlToTreeNodeSummaryMapper>().CreateInstance("/testpage");

			Assert.AreEqual(pageId.ToString(), result.Id);
		}

		[TestMethod]
		public void Returns_correct_tree_node_summary_when_passed_a_url_2_deep()
		{
			var page1Id = "page1Id";
			var page2Id = "page2Id";
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(Constants.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = page1Id,
										UrlSegment = "testpage",
										MayHaveChildNodes = true
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = Guid.NewGuid().ToString(),
										UrlSegment = "testSubPage",
										MayHaveChildNodes = true
				         			},
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(page1Id))
				.Returns(new TreeNodeSummary[]
				         	{
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = page1Id,
				         				Id = page2Id,
										UrlSegment = "testSubPage",
										MayHaveChildNodes = true
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = page1Id,
										UrlSegment = "testSubPage2",
										MayHaveChildNodes = true
				         			},
							});

			var result = mocker.Resolve<UrlToTreeNodeSummaryMapper>().CreateInstance("/testpage/testSubPage");

			Assert.AreEqual(page2Id.ToString(), result.Id);
		}

		[TestMethod]
		public void Returns_correct_tree_node_summary_when_passed_a_url_2_deep_with_a_querystring()
		{
			var page1Id = "page1Id";
			var page2Id = "page2Id";
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(Constants.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = page1Id,
										UrlSegment = "testpage",
										MayHaveChildNodes = true
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = Guid.NewGuid().ToString(),
										UrlSegment = "testSubPage",
										MayHaveChildNodes = true
				         			},
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(page1Id))
				.Returns(new TreeNodeSummary[]
				         	{
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = page1Id,
				         				Id = page2Id,
										UrlSegment = "testSubPage",
										MayHaveChildNodes = true
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = page1Id,
										UrlSegment = "testSubPage2",
										MayHaveChildNodes = true
				         			},
							});

			var result = mocker.Resolve<UrlToTreeNodeSummaryMapper>().CreateInstance("/testpage/testSubPage?2q34sda=sdf&asasd;f");

			Assert.AreEqual(page2Id, result.Id);
		}

		[TestMethod]
		public void Returns_correct_tree_node_summary_when_passed_a_url_to_a_controller_action()
		{
			var page1Id = "page1Id";
			var page2Id = "page2Id";
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(Constants.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = page1Id,
										UrlSegment = "testpage",
										MayHaveChildNodes = true
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = Constants.RootNodeId,
				         				Id = Guid.NewGuid().ToString(),
										UrlSegment = "testSubPage",
										MayHaveChildNodes = false
				         			},
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(page1Id))
				.Returns(new TreeNodeSummary[]
				         	{
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = page1Id,
				         				Id = page2Id,
										UrlSegment = "testSubPage",
										MayHaveChildNodes = false
				         			},
								new TreeNodeSummary()
				         			{
										ParentTreeNodeId = page1Id,
										UrlSegment = "testSubPage2",
										MayHaveChildNodes = true
				         			},
							});

			var result = mocker.Resolve<UrlToTreeNodeSummaryMapper>().CreateInstance("/testpage/testSubPage/actionName");

			Assert.AreEqual(page2Id, result.Id);
		}
	}
}
