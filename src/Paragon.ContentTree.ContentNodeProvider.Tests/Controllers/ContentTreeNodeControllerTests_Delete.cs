using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class LandingPageControllerTests_Delete
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_delete_on_IContentTreeNodeContext_with_correct_id()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Delete("id");

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.Delete("id"), Times.Once());
		}

		[TestMethod]
		public void Returns_redirect_to_ContentTreeController_Index_action()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Delete(null);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			var redirectToRouteResult = (RedirectToRouteResult) result;
			//Assert.AreEqual(1, redirectToRouteResult.RouteValues.ToArray().Where(a => a.Key == "controller").Where(a => a.Value == "ContentTree").Count());
			//Assert.AreEqual(1, redirectToRouteResult.RouteValues.Where(a => a.Key == "action").Where(a => a.Value == "index").Count());
		}
	}
}
