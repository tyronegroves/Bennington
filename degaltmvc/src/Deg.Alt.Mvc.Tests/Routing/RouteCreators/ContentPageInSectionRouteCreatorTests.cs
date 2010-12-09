using System.Web;
using System.Web.Routing;
using AutoMoq;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using Deg.Alt.Mvc.Routing.RouteCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Routing.RouteCreators
{
    [TestClass]
    public class ContentPageInSectionRouteCreatorTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();

            mocker.SetupFakeHttpContext();

            SetupRouteConstraintToReturn(true);
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
        public void CreateRoute_PassedSectionIdAndPageId_Match()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Section/Page");

            // assert
            Assert.IsNotNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_ConstraintReturnsFalse_DoesNotMatch()
        {
            // arrange
            SetupRouteConstraintToReturn(false);

            // act
            var routeData = GetRouteDataForUrl("~/Section/Page");

            // assert
            Assert.IsNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_SectionIsTest_SetsTestToSectionId()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Test/Page");

            // assert
            Assert.AreEqual("Test", routeData.Values["sectionId"]);
        }

        [TestMethod]
        public void CreateRoute_PageIsTest_SetsTestToController()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Section/Test");

            // assert
            Assert.AreEqual("Test", routeData.Values["controller"]);
        }

        [TestMethod]
        public void CreateRoute_PassedOnlySectionAndPage_DefaultsActionToIndex()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Section/Page");

            // assert
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void CreateRoute_PassedTestAsAction_SetsTestToAction()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Section/Page/Test");

            // assert
            Assert.AreEqual("Test", routeData.Values["action"]);
        }

        [TestMethod]
        public void CreateRoute_IdIsTest_SetsTestToId()
        {
            // act
            var routeData = GetRouteDataForUrl("~/Section/Page/Action/Test");

            // assert
            Assert.AreEqual("Test", routeData.Values["id"]);
        }

        private ContentPageInSectionRouteCreator GetRouteCreator()
        {
            return new ContentPageInSectionRouteCreator(mocker.GetMock<IContentPageInSectionRouteConstraint>().Object,
                                                        new FakeServiceLocator());
        }

        private void SetupRouteConstraintToReturn(bool returnValue)
        {
            mocker.GetMock<IContentPageInSectionRouteConstraint>()
                .Setup(x => x.Match(It.IsAny<HttpContextBase>(), It.IsAny<Route>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>(), It.IsAny<RouteDirection>()))
                .Returns(returnValue);
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