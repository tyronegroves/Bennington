using System.Web;
using System.Web.Routing;
using AutoMoq;
using Deg.Alt.Mvc.Routing.RouteCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Routing.RouteCreators
{
    [TestClass]
    public class RootPageRouteCreatorTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();

            mocker.SetupFakeHttpContext();
        }

        [TestMethod]
        public void CreateRoute_Called_ReturnsRoute()
        {
            // arrange
            var action = GetRouteCreator();

            // act
            var route = action.CreateRoute();

            // assert
            Assert.IsNotNull(route);
        }

        [TestMethod]
        public void CreateRoute_PassedUrlThatIsNotEmpty_NoMatch()
        {
            // act
            var routeData = GetRouteDataForUrl("~/not empty string");

            // assert
            Assert.IsNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_PassedUrlThatIsEmpty_Match()
        {
            // act
            var routeData = GetRouteDataForUrl("~/");

            // assert
            Assert.IsNotNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_PassedUrl_ReturnsIndex_AsPageId()
        {
            // act
            var routeData = GetRouteDataForUrl("~/");

            // assert
            Assert.AreEqual("Index_", routeData.Values["pageId"]);
        }

        [TestMethod]
        public void CreateRoute_PassedUrl_ReturnsEmptySectionId()
        {
            // act
            var routeData = GetRouteDataForUrl("~/");

            // assert
            Assert.AreEqual("", routeData.Values["sectionId"]);
        }

        [TestMethod]
        public void CreateRoute_PassedUrl_ReturnsIndexAction()
        {
            // act
            var routeData = GetRouteDataForUrl("~/");

            // assert
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        private RootPageRouteCreator GetRouteCreator()
        {
            return new RootPageRouteCreator(new FakeServiceLocator());
        }

        private RouteData GetRouteDataForUrl(string url)
        {
            var routeCollection = new RouteCollection();

            var action = GetRouteCreator();
            routeCollection.Add(action.CreateRoute());

            mocker.SetupFakeHttpRequest(url);
            var httpContext = mocker.GetMock<HttpContextBase>().Object;

            return routeCollection.GetRouteData(httpContext);
        }
    }
}