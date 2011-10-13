using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Controllers;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Data;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Repositories;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.ViewModelBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ToolLinkProviderNodeControllerTests_Modify
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();

            mocker.GetMock<IToolLinkProviderDraftRepository>()
                .Setup(a => a.GetAll())
                .Returns(new ToolLinkProviderDraft[]
		                                 {
		                                     new ToolLinkProviderDraft()
		                                         {
		                                             Id = "id",
		                                         }, 
		                                 }.AsQueryable());
		}

		[TestMethod]
		public void Returns_correct_view_name()
		{
			var result = mocker.Resolve<ToolLinkProviderNodeController>().Modify("id") as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_model_from_view_model_builder()
		{
			mocker.GetMock<IModifyViewModelBuilder>().Setup(a => a.BuildViewModel(It.IsAny<ToolLinkInputModel>()))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<ToolLinkProviderNodeController>().Modify("id") as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_input_model_with_TreeNodeId_set_to_tree_node_id_passed_in()
		{
			mocker.Resolve<ToolLinkProviderNodeController>().Modify("id");

			mocker.GetMock<IModifyViewModelBuilder>().Verify(a => a.BuildViewModel(It.Is<ToolLinkInputModel>(b => b.TreeNodeId == "id")), Times.Once());
		}

	}
}
