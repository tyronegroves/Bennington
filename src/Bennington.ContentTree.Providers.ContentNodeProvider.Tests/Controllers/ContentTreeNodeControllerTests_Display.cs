using System.Web.Mvc;
using System.Web.Routing;
using AutoMoq;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Controllers
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
			mocker.GetMock<IContentTreeNodeDisplayViewModelBuilder>().Setup(a => a.BuildViewModel("url", It.IsAny<RouteData>(), It.IsAny<string>()))
				.Returns(expectedViewModel);

			var result = (ContentTreeNodeDisplayViewModel) ((ViewResult) mocker.Resolve<ContentTreeNodeController>().Display("controller", "action")).ViewData.Model;

			Assert.AreEqual(expectedViewModel, result);
		}

		[TestMethod]
		public void Returns_view_name()
		{
			var result = mocker.Resolve<ContentTreeNodeController>().Display("controller", "action");

			Assert.AreEqual("Display", ((ViewResult)result).ViewName);
		}
	}
}
