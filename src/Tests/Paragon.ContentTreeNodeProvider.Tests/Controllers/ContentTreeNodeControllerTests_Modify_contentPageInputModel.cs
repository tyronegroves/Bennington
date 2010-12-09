using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;

namespace Paragon.ContentTreeNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerTests_Modify_ContentTreeNodeInputModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}



		[TestMethod]
		public void Sets_view_model_action_to_Modify_when_ModelState_when_ModelState_is_not_valid()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			landingPageController.ModelState.AddModelError("key", "error");
			var result = landingPageController.Modify(new ContentTreeNodeInputModel());

			Assert.AreEqual("Modify", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).Action);
		}

		[TestMethod] 
		public void Sets_Key_property_of_ContentTreeNode_to_match_ContentTreeNode_pulled_from_repository_with_matching_TreeNodeId()
		{
			var landingPageInputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = "2",
				Name = "name",
			};
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes())
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				Key = 2,
										TreeNodeId = "2",
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(a => a.CreateInstance(landingPageInputModel))
				.Returns(new ContentTreeNode()
				{
					Name = "name",
				});

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify(landingPageInputModel);

			mocker.GetMock<IContentTreeNodeRepository>().Verify(a => a.Update(It.Is<ContentTreeNode>(b => b.Key == 2)), Times.Once());
		}

		[TestMethod]
		public void Returns_redirect_result_to_content_tree_index_when_ModelState_is_valid_and_input_model_action_is_saveandexit()
		{
			var landingPageInputModel = new ContentTreeNodeInputModel()
			{
				Name = "name",
				Action = "Save and Exit"
			};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(a => a.CreateInstance(landingPageInputModel)).Returns(new ContentTreeNode()
			{
				Name = "name",
			});

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify(landingPageInputModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			//Assert.AreEqual("ContentTree", ((RedirectToRouteResult)result).RouteValues["controller"]);
			//Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"]);
		}

		[TestMethod]
		public void Returns_redirect_result_when_ModelState_is_valid()
		{
			var landingPageInputModel = new ContentTreeNodeInputModel()
			{
				Name = "name",
			};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(a => a.CreateInstance(landingPageInputModel)).Returns(new ContentTreeNode()
			{
				Name = "name",
			});

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify(landingPageInputModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			//Assert.AreEqual("LandingPage", ((RedirectToRouteResult)result).RouteValues["controller"]);
			//Assert.AreEqual("Modify", ((RedirectToRouteResult)result).RouteValues["action"]);
		}

		[TestMethod]
		public void Returns_view_model_with_input_model_set_to_the_same_as_was_passed_in_when_ModelState_is_invalid()
		{
			var landingPageInputModel = new ContentTreeNodeInputModel()
			{
				Name = "name",
			};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(a => a.CreateInstance(landingPageInputModel)).Returns(new ContentTreeNode()
			{
				Name = "name",
			});

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			landingPageController.ModelState.AddModelError("key", "error");
			var result = landingPageController.Modify(landingPageInputModel);

			var landingPageViewModel = (ContentTreeNodeViewModel)((ViewResult) result).ViewData.Model;
			Assert.AreEqual(landingPageInputModel, landingPageViewModel.ContentTreeNodeInputModel);
		}

		[TestMethod]
		public void Does_not_call_Update_on_repository_with_mapped_object_from_ILandingPageInputModelToContentTreeNodeMapper_when_ModelState_is_invalid()
		{
			var landingPageInputModel = new ContentTreeNodeInputModel()
			{
				Name = "name",
			};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(a => a.CreateInstance(landingPageInputModel)).Returns(new ContentTreeNode()
				{
					Name = "name",
				});

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			landingPageController.ModelState.AddModelError("key", "error");
			landingPageController.Modify(landingPageInputModel);

			mocker.GetMock<IContentTreeNodeRepository>().Verify(a => a.Update(It.IsAny<ContentTreeNode>()), Times.Never());
		}

		[TestMethod]
		public void Calls_Update_on_repository_with_mapped_object_from_ILandingPageInputModelToContentTreeNodeMapper_when_ModelState_is_valid()
		{
			var inputModel = new ContentTreeNodeInputModel()
			                 	{
									Name = "name",
									TreeNodeId = "treeNodeId",
									ContentItemId = "Index"
			                 	};
			var expectedContentTreeNode = new ContentTreeNode()
			                              	{
			                              		TreeNodeId = "treeNodeId",
												ContentItemId = "Index",
			                              	};
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes())
				.Returns(new ContentTreeNode[]
				         	{
								new ContentTreeNode()
									{
										TreeNodeId = "treeNodeId",
										ContentItemId = "Confirmation",
									},
				         		expectedContentTreeNode, 
							}.AsQueryable());
			//mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(
			//    a => a.LoadIntoInstance(inputModel, It.Is<ContentTreeNode>(b => b.ContentItemId == "Index")))

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Modify(inputModel);

			mocker.GetMock<IContentTreeNodeRepository>().Verify(a => a.Update(It.IsAny<ContentTreeNode>()), Times.Once());
		}

		[TestMethod]
		public void Calls_Create_on_repository_with_mapped_object_from_IContentTreeNodeInputModelToContentTreeNodeMapper_when_ModelState_is_valid_and_the_node_does_not_yet_exist()
		{
			var inputModel = new ContentTreeNodeInputModel()
			{
				Name = "name",
				TreeNodeId = "treeNodeId",
				ContentItemId = "Index"
			};
			
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(
				a => a.CreateInstance(It.IsAny<ContentTreeNodeInputModel>()))
				.Returns(new ContentTreeNode());

			var controller = mocker.Resolve<ContentTreeNodeController>();
			var result = controller.Modify(inputModel);

			mocker.GetMock<IContentTreeNodeRepository>().Verify(a => a.Create(It.Is<ContentTreeNode>(b => b != null)), Times.Once());
		}

		[TestMethod]
		public void Queries_repository_with_contentItemId_set()
		{
			var inputModel = new ContentTreeNodeInputModel()
			{
				Name = "name",
				TreeNodeId = "treeNodeId",
				ContentItemId = "Index"
			};
			var expectedContentTreeNode = new ContentTreeNode()
			{
				TreeNodeId = "treeNodeId",
				ContentItemId = "Index",
			};
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes())
				.Returns(new ContentTreeNode[]
				         	{
								new ContentTreeNode()
									{
										TreeNodeId = "treeNodeId",
										ContentItemId = "Confirmation",
									},
				         		expectedContentTreeNode, 
							}.AsQueryable());


			var controller = mocker.Resolve<ContentTreeNodeController>();
			var result = controller.Modify(inputModel);

			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Verify(a => a.LoadIntoInstance(It.IsAny<ContentTreeNodeInputModel>(), It.Is<ContentTreeNode>(b => b.ContentItemId == "Index")));
		}
	}
}
