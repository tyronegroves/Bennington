using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ToolLinkNodeProvider.Controllers;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;
using Paragon.ContentTree.ToolLinkNodeProvider.ViewModelBuilders;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ToolLinkProviderNodeControllerTests_Create_post_method
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_view_name_when_model_state_is_invalid()
		{
			var result = GetToolLinkProviderNodeControllerWithInvalidModelState().Create(new ToolLinkInputModel()) as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		private ToolLinkProviderNodeController GetToolLinkProviderNodeControllerWithInvalidModelState()
		{
			var controller = mocker.Resolve<ToolLinkProviderNodeController>();
			controller.ModelState.AddModelError("key", "error");
			return controller;
		}

		[TestMethod]
		public void Returns_view_model_from_view_model_builder_when_model_state_is_invalid()
		{
			mocker.GetMock<IModifyViewModelBuilder>().Setup(a => a.BuildViewModel(It.IsAny<ToolLinkInputModel>()))
				.Returns(new ModifyViewModel());

			var result = GetToolLinkProviderNodeControllerWithInvalidModelState().Create(new ToolLinkInputModel()) as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_input_model_passed_into_controller_to_view_model_builder_when_model_state_is_invalid()
		{
			GetToolLinkProviderNodeControllerWithInvalidModelState().Create(new ToolLinkInputModel()
			                                                                	{
			                                                                		Name = "test"
			                                                                	});

			mocker.GetMock<IModifyViewModelBuilder>().Verify(a => a.BuildViewModel(It.Is<ToolLinkInputModel>(b => b.Name == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_redirect_to_when_model_state_is_valid()
		{
			var result = mocker.Resolve<ToolLinkProviderNodeController>().Create(new ToolLinkInputModel()) as RedirectResult;

			Assert.IsNotNull(result);
		}
	}
}
