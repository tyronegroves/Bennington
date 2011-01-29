using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.Cms.Controllers;
using Paragon.Cms.ViewModelBuilders;
using Paragon.ContentTree.Models;

namespace Paragon.Cms.Tests.Controllers
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

			var contentTreeController = mocker.Resolve<ContentTreeController>();
			var result = contentTreeController.Branch(null);

			Assert.AreSame(viewModel, ((ViewResult)result).ViewData.Model);
		}
	}
}
