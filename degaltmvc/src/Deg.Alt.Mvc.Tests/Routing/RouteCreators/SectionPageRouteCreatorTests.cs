using System.Web;
using System.Web.Routing;
using AutoMoq;
using Deg.Alt.Mvc.Routing.RouteCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Deg.Alt.Mvc.Tests.Routing.RouteCreators
{
    [TestClass]
    public class SectionPageRouteCreatorTests
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
            var action = GetTheSectionPageRouteCreator();
            var route = action.CreateRoute();
            Assert.IsNotNull(route);
        }

        [TestMethod]
        public void CreateRoute_PassedSection_Match()
        {
            var routeData = GetRouteDataForUrl("~/Section");
            Assert.IsNotNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_PassedSectionAndAction_Match()
        {
            var routeData = GetRouteDataForUrl("~/Section/Action");
            Assert.IsNotNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_PassedSectionAndActionAndId_Match()
        {
            var routeData = GetRouteDataForUrl("~/Section/Action/Id");
            Assert.IsNotNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_PassedFourItemsInUrl_DoesNotMatch()
        {
            var routeData = GetRouteDataForUrl("~/One/Two/Three/Four");
            Assert.IsNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_ConstraintFails_DoesNotMatch()
        {
            SetupRouteConstraintToReturn(false);
            var routeData = GetRouteDataForUrl("~/Section");
            Assert.IsNull(routeData);
        }

        [TestMethod]
        public void CreateRoute_PassedTestAsSection_SetsSectionIdToTest()
        {
            var routeData = GetRouteDataForUrl("~/TEST");
            Assert.AreEqual("TEST", routeData.Values["sectionId"]);
        }

        [TestMethod]
        public void CreateRoute_PassedTestAsAction_SetsActionToTest()
        {
            var routeData = GetRouteDataForUrl("~/Section/TEST");
            Assert.AreEqual("TEST", routeData.Values["action"]);
        }

        [TestMethod]
        public void CreateRoute_PassedTestAsId_SetsIdToTest()
        {
            var routeData = GetRouteDataForUrl("~/Section/Action/TEST");
            Assert.AreEqual("TEST", routeData.Values["id"]);
        }

        private SectionPageRouteCreator GetTheSectionPageRouteCreator()
        {
            return new SectionPageRouteCreator(mocker.GetMock<ISectionRouteConstraint>().Object,
                                               new FakeServiceLocator());
        }

        private void SetupRouteConstraintToReturn(bool returnValue)
        {
            mocker.GetMock<ISectionRouteConstraint>()
                .Setup(x => x.Match(It.IsAny<HttpContextBase>(), It.IsAny<Route>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>(), It.IsAny<RouteDirection>()))
                .Returns(returnValue);
        }

        private RouteData GetRouteDataForUrl(string url)
        {
            var routeCollection = new RouteCollection();

            var creator = GetTheSectionPageRouteCreator();
            routeCollection.Add(creator.CreateRoute());

            mocker.SetupFakeHttpRequest(url);
            var httpContext = mocker.GetMock<HttpContextBase>().Object;

            return routeCollection.GetRouteData(httpContext);
        }
    }
}