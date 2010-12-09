using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.ViewModelBuilders.Helpers;
using Paragon.ContentTreeNodeProvider.Context;
using Paragon.ContentTreeNodeProvider.Data;
using Paragon.ContentTreeNodeProvider.ViewModelBuilders;
using Paragon.Pages.Adapters;

namespace Paragon.LandingPageContentTreeNodeProvider.Tests.ViewModelBuilders
{
	[TestClass]
	public class ContentTreeNodeDisplayViewModelBuilderTests_BuildViewModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_Header_for_tree_node_url_when_url_contains_querystring_data()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(ContentTreeNodeContext.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "1",
										UrlSegment = "test",
										ParentTreeNodeId = ContentTreeNodeContext.RootNodeId,
				         			}, 
							});
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("1"))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				ContentItemId = "Index",
										Content = "page content1",
										Name = "test1"
				         			}, 
							});
			var routeData = new RouteData();
			routeData.Values.Add("Action", "Index");

			var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel("test?a=1", routeData);

			Assert.AreEqual("test1", result.Header);
		}

		[TestMethod]
		public void Returns_correct_Header_value_for_second_level_tree_node_url_when_url_case_does_not_match()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(ContentTreeNodeContext.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "1",
										UrlSegment = "Section1",
										ParentTreeNodeId = ContentTreeNodeContext.RootNodeId,
				         			}, 
							});
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("1"))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				ContentItemId = "Index",
										Content = "page content",
										Name = "section1"
				         			}, 
							});
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren("1"))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "2",
										UrlSegment = "test"
				         			}, 
							});
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("2"))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				ContentItemId = "Index",
										Content = "page1 content",
										Name = "page1"
				         			}, 
							});
			var routeData = new RouteData();
			routeData.Values.Add("Action", "Index");

			var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel("SECTION1/TEST", routeData);

			Assert.AreEqual("page1", result.Header);
		}

		[TestMethod]
		public void Returns_correct_Header_value_for_first_level_tree_node_url_when_url_case_does_not_match()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(ContentTreeNodeContext.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "1",
										UrlSegment = "Section1"
				         			}, 
							});
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("1"))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				ContentItemId = "Index",
										Content = "page content",
										Name = "section1"
				         			}, 
							});
			var routeData = new RouteData();
			routeData.Values.Add("Action", "Index");

			var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel("SECTION1", routeData);

			Assert.AreEqual("section1", result.Header);
		}

		[TestMethod]
		public void Returns_correct_Header_value_for_first_level_tree_node_url()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(ContentTreeNodeContext.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "1",
										UrlSegment = "Section1"
				         			}, 
							});
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("1"))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				ContentItemId = "Index",
										Content = "page content",
										Name = "section1"
				         			}, 
							});
			var routeData = new RouteData();
			routeData.Values.Add("Action", "Index");

			var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel("Section1/", routeData);

			Assert.AreEqual("section1", result.Header);
		}

		[TestMethod]
		public void Returns_correct_Body_value_for_first_level_tree_node_url()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(ContentTreeNodeContext.RootNodeId))
				.Returns(new TreeNodeSummary[]
				         	{
				         		new TreeNodeSummary()
				         			{
				         				Id = "1",
										UrlSegment = "Section1"
				         			}, 
							});
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("1"))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				ContentItemId = "Index",
										Content = "page content",
				         			}, 
							});
			var routeData = new RouteData();
			routeData.Values.Add("Action", "Index");

			var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel("Section1/", routeData);

			Assert.AreEqual("page content", result.Body);
		}

		[TestMethod]
		public void Returns_correct_Body_value_for_first_level_tree_node_url_when_there_are_multiple_ContentTreeNodes_for_the_current_url_and_the_action_is_Index2()
		{
		    mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.GetChildren(ContentTreeNodeContext.RootNodeId))
		        .Returns(new TreeNodeSummary[]
		                    {
		                        new TreeNodeSummary()
		                            {
		                                Id = "1",
		                                UrlSegment = "Section1"
		                            }, 
		                    });
			mocker.GetMock<IGetParentRouteDataDictionaryFromChildActionRouteData>()
				.Setup(a => a.GetRouteValues(It.IsAny<RouteData>()))
				.Returns((RouteData q) => q);
		    mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId("1"))
		        .Returns(new ContentTreeNode[]
		                    {
		                        new ContentTreeNode()
		                            {
		                                ContentItemId = "Index",
		                                Content = "page content",
		                            }, 
		                        new ContentTreeNode()
		                            {
		                                ContentItemId = "Index2",
		                                Content = "page content2",
		                            },
		                        new ContentTreeNode()
		                            {
		                                ContentItemId = "Index3",
		                                Content = "page content3",
		                            },
		                    });
			var routeData = new RouteData();
			routeData.Values.Add("Action", "Index2");

		    var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel("Section1/", routeData);

		    Assert.AreEqual("page content2", result.Body);
		}

		[TestMethod]
		public void Returns_emtpy_view_model_when_url_doesnt_match_a_tree_node_url()
		{
			var result = mocker.Resolve<ContentTreeNodeDisplayViewModelBuilder>().BuildViewModel(null, null);

			Assert.AreEqual(string.Empty, result.Header);
			Assert.AreEqual(string.Empty, result.Body);
		}
	}
}
