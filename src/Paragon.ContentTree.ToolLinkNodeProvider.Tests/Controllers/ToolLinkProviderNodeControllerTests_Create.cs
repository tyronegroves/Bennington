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
	public class ToolLinkProviderNodeControllerTests_Create
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_view_name()
		{
			var result = mocker.Resolve<ToolLinkProviderNodeController>().Create((string) null) as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_model_from_view_model_builder()
		{
			mocker.GetMock<IModifyViewModelBuilder>().Setup(a => a.BuildViewModel(It.IsAny<ToolLinkInputModel>()))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<ToolLinkProviderNodeController>().Create((string) null) as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_input_model_with_ParentTreeNodeId_set_into_view_model_builder()
		{
			mocker.Resolve<ToolLinkProviderNodeController>().Create("parentTreeNodeId");

			mocker.GetMock<IModifyViewModelBuilder>().Verify(a => a.BuildViewModel(It.Is<ToolLinkInputModel>(b => b.ParentTreeNodeId == "parentTreeNodeId")), Times.Once());
		}

		[TestMethod]
		public void Passes_input_model_with_Action_set_to_Create()
		{
			mocker.Resolve<ToolLinkProviderNodeController>().Create((string)null);

			mocker.GetMock<IModifyViewModelBuilder>().Verify(a => a.BuildViewModel(It.Is<ToolLinkInputModel>(b => b.Action == "Create")), Times.Once());
		}
	}
}
