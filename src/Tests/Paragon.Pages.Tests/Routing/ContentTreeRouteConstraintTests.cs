using System.Web.Routing;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.Routing.Routing;

namespace Paragon.Pages.Tests.Routing
{
    [TestClass]
    public class ContentTreeRouteConstraintTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void SetupMocksForAllTests()
        {
            mocker = new AutoMoqer();
        }

        private ContentTreeRouteConstraint CreateContentTreeRouteConstraint()
        {
            return mocker.Resolve<ContentTreeRouteConstraint>();
        }
    }
}