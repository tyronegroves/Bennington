using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerTests_Create_contentPageInputModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sets_ContentItemId_property_of_inputModel_to_Index_if_blank()
		{
			var contentPageInputMOdel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.Create(contentPageInputMOdel);

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.CreateTreeNodeAndReturnTreeNodeId(It.Is<ContentTreeNodeInputModel>(b => b.ContentItemId == "Index")), Times.Once());
		}

		[TestMethod]
		public void Returns_RedirectResult_when_ModelState_is_valid()
		{
			var landingPageInputMOdel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Create(landingPageInputMOdel);

			Assert.IsInstanceOfType(result, typeof(RedirectResult));
		}

		[TestMethod]
		public void Returns_view_model_with_input_model_set_to_same_input_model_that_was_passed_in_when_ModelState_is_invalid()
		{
			var landingPageInputMOdel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};

			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			landingPageController.ModelState.AddModelError("key", "error");
			var result = landingPageController.Create(landingPageInputMOdel);

			Assert.AreEqual(landingPageInputMOdel, ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel);
		}

		[TestMethod]
		public void Calls_Create_method_of_IContentTreeNodeContext_when_ModelState_is_valid()
		{
			var landingPageInputMOdel = new ContentTreeNodeInputModel()
			                            	{
			                            		ParentTreeNodeId = "2",
			                            	};
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Create(landingPageInputMOdel);

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.CreateTreeNodeAndReturnTreeNodeId(landingPageInputMOdel), Times.Once());
		}

		[TestMethod]
		public void Does_not_call_Create_method_of_IContentTreeNodeContext_when_ModelState_is_invalid()
		{
			var landingPageInputMOdel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			landingPageController.ModelState.AddModelError("key", "error");
			var result = landingPageController.Create(landingPageInputMOdel);

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.CreateTreeNodeAndReturnTreeNodeId(landingPageInputMOdel), Times.Never());
		}

		[TestMethod]
		public void Sets_view_model_action_to_create_when_ModelState_is_not_valid()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			landingPageController.ModelState.AddModelError("key", "error");
			var result = landingPageController.Create(new ContentTreeNodeInputModel());

			Assert.AreEqual("Create", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).Action);
		}
	}
}
