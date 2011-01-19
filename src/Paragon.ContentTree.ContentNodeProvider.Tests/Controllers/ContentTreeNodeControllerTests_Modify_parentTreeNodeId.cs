using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerControllerTests_Modify_parentTreeNodeId_contentItemId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker= new AutoMoqer();
		}

		[TestMethod]
		public void Sets_view_model_action_to_Modify()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("", null);

			Assert.AreEqual("Modify", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).Action);
		}

		[TestMethod]
		public void Sets_input_model_ContentItemId_to_Index_when_content_tree_node_pulled_from_repository_is_null()
		{
			var expectedInputModel = new ContentTreeNodeInputModel()
			{
				Name = "some name",
			};
			mocker.GetMock<IContentTreeNodeToContentTreeNodeInputModelMapper>().Setup(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.TreeNodeId == "1" && b.ContentItemId == "Index")))
				.Returns(expectedInputModel);

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("1", null);

			var viewModel = (ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model;
			Assert.AreEqual("Index", viewModel.ContentTreeNodeInputModel.ContentItemId);
		}

		[TestMethod]
		public void Sets_input_model_ContentItemId_to_value_passed_in_when_the_content_tree_node_is_not_returned_from_the_repository()
		{
			var expectedInputModel = new ContentTreeNodeInputModel()
			{
				Name = "some name",
				ContentItemId = "contentItemId"
			};

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("1", "contentItemId");

			Assert.AreEqual("contentItemId", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel.ContentItemId);
		}


		[TestMethod]
		public void Uses_Index_as_ContentItemId_when_content_item_id_is_not_passed()
		{
			var expectedInputModel = new ContentTreeNodeInputModel()
			{
				Name = "some name",
			};
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes()).Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = "1",
										Name = "zzz",
										ContentItemId = "Index",
				         			}, 
							}.AsQueryable());

			mocker.GetMock<IContentTreeNodeToContentTreeNodeInputModelMapper>().Setup(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.TreeNodeId == "1" && b.ContentItemId == "Index")))
				.Returns(expectedInputModel);

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Modify("1", null);

			mocker.GetMock<IContentTreeNodeToContentTreeNodeInputModelMapper>().Verify(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.ContentItemId == "Index")), Times.Once());
		}

		[TestMethod]
		public void Sets_input_model_from_content_tree_node_pulled_from_repository()
		{
			var expectedInputModel = new ContentTreeNodeInputModel()
			                         	{
											Name = "some name",
			                         	};
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes())
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = "1",
										Name = "zzz",
										ContentItemId = "zzz",
				         			}, 
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = "1",
										Name = "some name",
										ContentItemId = "Index",
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IContentTreeNodeToContentTreeNodeInputModelMapper>().Setup(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.TreeNodeId == "1" && b.ContentItemId == "Index")))
				.Returns(expectedInputModel);

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("1", "Index");

			var viewModel = (ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model;
			Assert.AreEqual("some name", viewModel.ContentTreeNodeInputModel.Name);
		}

		[TestMethod]
		public void Sets_TreeNodeId_of_input_model_to_id_passed_to_Modify()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("0", null);

			var viewModel = (ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model;
			Assert.AreEqual("0", viewModel.ContentTreeNodeInputModel.TreeNodeId);
		}

		[TestMethod]
		public void Returns_empty_view_model_with_empty_input_model_when_passed_0()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("0", null);

			var viewModel = (ContentTreeNodeViewModel) ((ViewResult) result).ViewData.Model;
			Assert.IsNotNull(viewModel.ContentTreeNodeInputModel);
		}

		[TestMethod]
		public void Returns_empty_view_model_when_passed_0()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify("0", null);

			Assert.IsInstanceOfType(((ViewResult)result).ViewData.Model, typeof(ContentTreeNodeViewModel));
		}
	}
}
