using System.Linq;
using System.Web.Routing;
using Deg.Alt.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Deg.Alt.Mvc.Tests.Routing
{
    [TestClass]
    public class RouteRegistratorTests
    {
        [TestMethod]
        public void Register_ProvidedOneRouteCreator_AddsRouteFromCreator()
        {
            // arrange
            var expectedRoute = new Route(string.Empty, null);

            var routeCollection = new RouteCollection();

            var registrator = new RouteRegistrator(new[]{GenerateRouteCreatorFake(expectedRoute).Object});

            // act
            registrator.Register(routeCollection);

            // assert
            Assert.AreEqual(1, routeCollection.Count);
            Assert.AreSame(expectedRoute, routeCollection.First());
        }

        [TestMethod]
        public void Register_ProvidedNoRouteCreators_DoesNotAddAnyRoutes()
        {
            // arrange
            var routeCollection = new RouteCollection();

            var registrator = new RouteRegistrator(new IRouteCreator[]{});

            // act
            registrator.Register(routeCollection);

            // assert
            Assert.AreEqual(0, routeCollection.Count);
        }

        [TestMethod]
        public void Register_ProvidedTwoRouteCreators_AddsRouteFromEachCreator()
        {
            // arrange
            var routeCollection = new RouteCollection();

            var expectedRoutes = new[]{new Route(string.Empty, null), new Route(string.Empty, null)}.ToList();

            var firstRouteCreator = GenerateRouteCreatorFake(expectedRoutes[0]);
            var secondRouteCreator = GenerateRouteCreatorFake(expectedRoutes[1]);

            var registrator = new RouteRegistrator(new[]{firstRouteCreator.Object, secondRouteCreator.Object});

            // act
            registrator.Register(routeCollection);

            // assert
            Assert.AreEqual(2, routeCollection.Count);
            expectedRoutes.ForEach(x => Assert.IsTrue(routeCollection.Contains(x)));
        }

        private static Mock<IRouteCreator> GenerateRouteCreatorFake(Route routeToReturn)
        {
            var routeCreatorFake = new Mock<IRouteCreator>();

            routeCreatorFake.Setup(x => x.CreateRoute())
                .Returns(routeToReturn);

            return routeCreatorFake;
        }
    }
}