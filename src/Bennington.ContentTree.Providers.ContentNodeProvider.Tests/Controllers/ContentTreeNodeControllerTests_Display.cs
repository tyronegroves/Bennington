using System.Web.Mvc;
using System.Web.Routing;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders;
using Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders.Helpers;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerTests_Display
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_view_model_from_viewModelBuilder()
		{
			mocker.GetMock<IRawUrlGetter>().Setup(a => a.GetRawUrl()).Returns("url");
			var expectedViewModel = new ContentTreeNodeDisplayViewModel()
			                        	{
			                        		Header = "Header",
			                        	};
			mocker.GetMock<IContentTreeNodeDisplayViewModelBuilder>().Setup(a => a.BuildViewModel("url", It.IsAny<RouteData>()))
				.Returns(expectedViewModel);

			var result = (ContentTreeNodeDisplayViewModel) ((ViewResult) mocker.Resolve<ContentTreeNodeController>().Index()).ViewData.Model;

			Assert.AreEqual(expectedViewModel, result);
		}

		[TestMethod]
		public void Returns_view_name()
		{
			var result = mocker.Resolve<ContentTreeNodeController>().Index();

			Assert.AreEqual("Index", ((ViewResult)result).ViewName);
		}
	}
}
