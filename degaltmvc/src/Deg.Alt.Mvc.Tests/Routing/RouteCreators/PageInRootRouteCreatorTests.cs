using System.Web;
using System.Web.Routing;
using AutoMoq;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using Deg.Alt.Mvc.Routing.RouteCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Deg.Alt.Mvc.Tests.Routing.RouteCreators
{
    [TestClass]
    public class PageInRootRouteCreatorTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();

            SetupRouteConstraintToReturn(true);

            mocker.SetupFakeHttpContext();
        }

        [TestMethod]
        public void CreateRoute_Called_ReturnsRoute()
        {
            // arrange
            var action = CreatePageInRootRouteCreator();

            // act
            var route = action.CreateRoute();

            // assert
            Assert.IsNotNull(route);
        }

        [TestMethod]
        public void CreateRoute_PassedSectionAndPage_Matches()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Page");

            // assert
            Assert.IsNotNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_ConstraintFails_DoesNotMatch()
        {
            // arrange
            SetupRouteConstraintToReturn(false);

            // act
            var routeData = GetRouteDataForUrl("~/Page");

            // assert
            Assert.IsNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_Called_ReturnsEmptyStringAsSectionId()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Test");

            // assert
            Assert.AreEqual(string.Empty, routeData.Values["sectionId"]);
        }

        [TestMethod]
        public void CreateRoute_NoActionSet_ReturnsIndex()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Test");

            // assert
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void CreateRoute_ActionIsTest_ReturnsTestAsAction()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Page/Test");

            // assert
            Assert.AreEqual("Test", routeData.Values["action"]);
        }

        [TestMethod]
        public void CreateRoute_IdIsTest_ReturnsTestAsId()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Page/Action/Test");

            // assert
            Assert.AreEqual("Test", routeData.Values["id"]);
        }

        [TestMethod]
        public void Returns_test_when_the_controller_is_test()
        {
            var routeData = GetRouteDataForUrl("~/TEST");
            Assert.AreEqual("TEST", routeData.Values["controller"]);
        }

        private void SetupRouteConstraintToReturn(bool returnValue)
        {
            mocker.GetMock<IPageInRootRouteConstraint>()
                .Setup(x => x.Match(It.IsAny<HttpContextBase>(), It.IsAny<Route>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>(), It.IsAny<RouteDirection>()))
                .Returns(returnValue);
        }

        private RouteData GetRouteDataForUrl(string url)
        {
            var routeCollection = new RouteCollection();

            var action = CreatePageInRootRouteCreator();
            routeCollection.Add(action.CreateRoute());

            mocker.SetupFakeHttpRequest(url);
            var httpContext = mocker.GetMock<HttpContextBase>().Object;

            return routeCollection.GetRouteData(httpContext);
        }

        private PageInRootRouteCreator CreatePageInRootRouteCreator()
        {
            return new PageInRootRouteCreator(mocker.GetMock<IPageInRootRouteConstraint>().Object, new FakeServiceLocator());
        }
    }
}