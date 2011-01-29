using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.TreeManager.Controllers;
using Paragon.ContentTree.TreeManager.Models;
using Paragon.ContentTree.TreeManager.ViewModelBuilders;

namespace Paragon.ContentTree.TreeManager.Tests.Controllers
{
	[TestClass]
	public class ContentTreeControllerTests_Branch
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
