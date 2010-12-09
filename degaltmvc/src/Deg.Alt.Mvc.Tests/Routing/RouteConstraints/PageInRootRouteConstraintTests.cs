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
    public class PageInRootRouteConstraintTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Match_PageExistsInRoot_ReturnsTrue()
        {
            // arrange
            mocker.GetMock<IGetPageForRoutingAction>()
                .Setup(x => x.GetPage("IdToMatch"))
                .Returns(new Page());

            var pageLocation = new PageLocation{SectionId = string.Empty, PageId = "IdToMatch"};
            var routeValueDictionary = SetupMapper(pageLocation);

            var constraint = mocker.Resolve<PageInRootRouteConstraint>();

            // act
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_PageDoesNotExist_ReturnsFalse()
        {
            // arrange
            mocker.GetMock<IGetPageForRoutingAction>()
                .Setup(x => x.GetPage("IdToMatch"))
                .Returns((Page)null);

            var pageLocation = new PageLocation{SectionId = string.Empty, PageId = "IdToMatch"};
            var routeValueDictionary = SetupMapper(pageLocation);

            var constraint = mocker.Resolve<PageInRootRouteConstraint>();

            // act
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_CurrentlyInSection_ReturnsFalse()
        {
            // arrange
            mocker.GetMock<IGetPageForRoutingAction>()
                .Setup(x => x.GetPage("IdToMatch"))
                .Returns(new Page());

            var pageLocation = new PageLocation{SectionId = "In a Section", PageId = "IdToMatch"};
            var routeValueDictionary = SetupMapper(pageLocation);

            var constraint = mocker.Resolve<PageInRootRouteConstraint>();

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