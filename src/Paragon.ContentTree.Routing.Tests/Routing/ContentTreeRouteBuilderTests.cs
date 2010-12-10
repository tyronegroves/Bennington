using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.Routing.Routing;

namespace Paragon.ContentTree.Routing.Tests.Routing
{
    [TestClass]
    public class ContentTreeRouteBuilderTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void SetupMocksForAllTests()
        {
            mocker = new AutoMoqer();
        }

        private ContentTreeRouteBuilder CreateContentTreeRouteBuilder()
        {
            return mocker.Resolve<ContentTreeRouteBuilder>();
        }
    }
}