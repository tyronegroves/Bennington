using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.Pages.Routing;

namespace Paragon.Pages.Tests.Routing
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