using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Actions
{
    [TestClass]
    public class GetPageForRoutingActionTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Returns_the_page_from_GetPageAction_when_the_controller_on_the_page_does_not_match_the_excluded_controllers()
        {
            mocker.GetMock<IControllersExcludedFromRoutingRegistry>()
                .Setup(x => x.GetExcludedControllers())
                .Returns(new[] {"BAD"});

            var page = new Page {Controller = "GOOD"};
            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("pageId"))
                .Returns(page);

            var action = mocker.Resolve<GetPageForRoutingAction>();
            var result = action.GetPage("pageId");

            Assert.AreSame(page, result);
        }

        [TestMethod]
        public void Returns_null_when_the_pageid_matches_an_excluded_controller()
        {
            mocker.GetMock<IControllersExcludedFromRoutingRegistry>()
                .Setup(x => x.GetExcludedControllers())
                .Returns(new[] {"BAD"});

            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("pageId"))
                .Returns(new Page {Controller = "BAD"});

            var action = mocker.Resolve<GetPageForRoutingAction>();
            var result = action.GetPage("pageId");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Returns_the_page_from_GetPageAction_when_the_controller_on_the_page_and_section_does_not_match_the_excluded_controllers()
        {
            mocker.GetMock<IControllersExcludedFromRoutingRegistry>()
                .Setup(x => x.GetExcludedControllers())
                .Returns(new[] {"BAD"});

            var page = new Page {Controller = "GOOD"};
            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("sectionId", "pageId"))
                .Returns(page);

            var action = mocker.Resolve<GetPageForRoutingAction>();
            var result = action.GetPage("sectionId", "pageId");

            Assert.AreSame(page, result);
        }

        [TestMethod]
        public void Returns_null_when_the_pageid_and_sectionid_matches_an_excluded_controller()
        {
            mocker.GetMock<IControllersExcludedFromRoutingRegistry>()
                .Setup(x => x.GetExcludedControllers())
                .Returns(new[] {"BAD"});

            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("sectionId", "pageId"))
                .Returns(new Page {Controller = "BAD"});

            var action = mocker.Resolve<GetPageForRoutingAction>();
            var result = action.GetPage("sectionId", "pageId");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Returns_null_when_the_pageid_returns_null()
        {
            mocker.GetMock<IControllersExcludedFromRoutingRegistry>()
                .Setup(x => x.GetExcludedControllers())
                .Returns(new[] {"BAD"});

            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("pageId"))
                .Returns((string p) => null);

            var action = mocker.Resolve<GetPageForRoutingAction>();
            var result = action.GetPage("pageId");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Returns_null_when_the_pageid_and_sectionid_returns_null()
        {
            mocker.GetMock<IControllersExcludedFromRoutingRegistry>()
                .Setup(x => x.GetExcludedControllers())
                .Returns(new[] {"BAD"});

            mocker.GetMock<IGetPageAction>()
                .Setup(x => x.GetPage("sectionId", "pageId"))
                .Returns((string s, string p) => null);

            var action = mocker.Resolve<GetPageForRoutingAction>();
            var result = action.GetPage("sectionId", "pageId");

            Assert.IsNull(result);
        }
    }
}