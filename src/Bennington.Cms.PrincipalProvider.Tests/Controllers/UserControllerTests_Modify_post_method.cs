using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Controllers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Services;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.PrincipalProvider.Tests.Controllers
{
	[TestClass]
	public class UserControllerTests_Modify_post_method
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_view_name_when_model_state_is_not_valid()
		{
			var controller = mocker.Resolve<UserController>();
			controller.ModelState.AddModelError("key", "error");

			var result = controller.Modify(new UserInputModel()) as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder_when_model_state_is_not_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<UserInputModel>()))
				.Returns(new ModifyViewModel());
			var controller = mocker.Resolve<UserController>();
			controller.ModelState.AddModelError("key", "error");

			var result = controller.Modify((UserInputModel) null) as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_input_model_into_view_model_builder_when_model_state_is_not_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<UserInputModel>()))
				.Returns(new ModifyViewModel());
			var controller = mocker.Resolve<UserController>();
			controller.ModelState.AddModelError("key", "error");

			controller.Modify(new UserInputModel()
			                                        	{
			                                        		FirstName = "test",
			                                        	});

			mocker.GetMock<IModifyViewModelBuilder>()
				.Verify(a => a.BuildViewModel(It.Is<UserInputModel>(b => b.FirstName == "test")), Times.Once());
		}

		[TestMethod]
		public void Calls_ProcessUserInputModelService_when_model_state_is_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<UserInputModel>()))
				.Returns(new ModifyViewModel());

			mocker.Resolve<UserController>().Modify(new UserInputModel()
			                                        	{
			                                        		Id = "test"
			                                        	});

			mocker.GetMock<IProcessUserInputModelService>()
				.Verify(a => a.ProcessAndReturnId(It.Is<UserInputModel>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_redirect_to_Modify_action_when_model_state_is_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<UserInputModel>()))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<UserController>().Modify(new UserInputModel()) as RedirectToRouteResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Does_not_call_ProcessUserInputModelService_when_model_state_is_not_valid()
		{
			var controller = mocker.Resolve<UserController>();
			controller.ModelState.AddModelError("key", "error");

			controller.Modify(new UserInputModel());

			mocker.GetMock<IProcessUserInputModelService>()
				.Verify(a => a.ProcessAndReturnId(It.IsAny<UserInputModel>()), Times.Never());
		}
	}
}
