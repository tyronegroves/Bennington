using System.Web;
using System.Web.Routing;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Mappers;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Routing.RouteConstraints
{
    [TestClass]
    public class ContentPageInSectionRouteConstraintTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();

            mocker.SetupFakeHttpContext();
        }

        [TestMethod]
        public void Match_SectionIdAndPageIdMatch_ReturnsTrue()
        {
            // arrange
            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("Match1", "Match2"))
                .Returns(new Page());

            var pageLocation = new PageLocation{SectionId = "Match1", PageId = "Match2"};
            var routeValueDictionary = SetupMapper(pageLocation);

            var constraint = mocker.Resolve<ContentPageInSectionRouteConstraint>();

            // act
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_SectionIdOrPageIdDoNotMatch_ReturnsFalse()
        {
            // arrange
            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("Match1", "Match2"))
                .Returns((Page)null);

            var pageLocation = new PageLocation{SectionId = "Match1", PageId = "Match2"};
            var routeValueDictionary = SetupMapper(pageLocation);

            var constraint = mocker.Resolve<ContentPageInSectionRouteConstraint>();

            // act
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            // assert
            Assert.IsFalse(result);
        }

        private RouteValueDictionary SetupMapper(PageLocation pageLocation)
        {
            var routeValueDictionary = new RouteValueDictionary();

            mocker.GetMock<IRouteValueDictionaryToPageLocationMapper>()
                .Setup(x => x.CreateInstance(routeValueDictionary))
                .Returns(pageLocation);
            return routeValueDictionary;
        }
    }
}