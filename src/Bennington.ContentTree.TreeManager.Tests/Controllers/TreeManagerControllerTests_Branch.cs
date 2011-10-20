using System.Web.Mvc;
using AutoMoq;
using Bennington.ContentTree.TreeManager.Controllers;
using Bennington.ContentTree.TreeManager.Models;
using Bennington.ContentTree.TreeManager.ViewModelBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.TreeManager.Tests.Controllers
{
	[TestClass]
	public class TreeManagerControllerTests_Branch
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker= new AutoMoqer();
		}

		[TestMethod]
		public void Returns_view_model_from_ITreeBranchViewModelBuilder()
		{
			var viewModel = new TreeBranchViewModel();
			mocker.GetMock<ITreeBranchViewModelBuilder>().Setup(a => a.BuildViewModel(It.IsAny<string>()))
				.Returns(viewModel);

			var contentTreeController = mocker.Resolve<TreeManagerController>();
			var result = contentTreeController.Branch(null);

			Assert.AreSame(viewModel, ((ViewResult)result).ViewData.Model);
		}
	}
}
