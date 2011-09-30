using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;
using Bennington.Cms.Tests.Mocks;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.Tests
{
    [Subject(typeof(ActionSectionMenuItem))]
    public class When_the_view_model_is_retrieved_from_an_ActionSectionMenuItem : ICleanupAfterEveryContextInAssembly
    {
        private Establish context = () =>
                                        {
                                            RouteTable.Routes.MapRoute(null, "testing/{controller}/{action}/fun"); 
                                            var httpContext = new MockHttpContext(new MockHttpRequest(), new MockResponseBase());
                                            var routeData = new RouteData();
                                            routeData.Values.Add("controller", "MyController");

                                            controllerContext = new ControllerContext { RequestContext = new RequestContext(httpContext, routeData), HttpContext = httpContext };

                                            sectionMenuItem = new ActionSectionMenuItem("My Name", "MyAction", "MyController", new {pp = "pp", gg = "gg"});
                                        };

        private Because of = () => { viewModel = sectionMenuItem.GetViewModel(controllerContext); };

        private It should_have_the_name_set = () => viewModel.Name.ShouldEqual("My Name");

        private It should_have_the_url_set = () => viewModel.Url.ShouldEqual("/testing/MyController/MyAction/fun?pp=pp&gg=gg");

        private static ActionSectionMenuItem sectionMenuItem;
        private static SectionMenuItemViewModel viewModel;
        private static ControllerContext controllerContext;

        private static ControllerContext GetControllerContext(object routeValues, object dataTokens)
        {
            var routeData = new RouteData();
            var values = new RouteValueDictionary(routeValues);
            var tokens = new RouteValueDictionary(dataTokens);

            foreach(var token in tokens)
                routeData.DataTokens.Add(token.Key, token.Value);

            foreach(var value in values)
                routeData.Values.Add(value.Key, value.Value);

            return new ControllerContext
                       {
                           RequestContext = new RequestContext {RouteData = routeData},
                           HttpContext = new MockHttpContext()
                       };
        }

        public void AfterContextCleanup()
        {
            using(RouteTable.Routes.GetWriteLock())
                RouteTable.Routes.Clear();
        }
    }

    [TestClass]
    public class ActionSectionMenuItemTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            using(RouteTable.Routes.GetWriteLock())
                RouteTable.Routes.Clear();
        }

        [TestMethod]
        public void When_the_controller_matches_the_current_route_values_the_section_menu_item_is_selected_method_should_return_true()
        {
            var actionSectionMenuItem = new ActionSectionMenuItem(null, "MyAction", "MyController");
            var httpContext = new MockHttpContext(new MockHttpRequest(), new MockResponseBase());
            var routeData = new RouteData();
            routeData.Values.Add("controller", "MyController");

            var controllerContext = new ControllerContext {RequestContext = new RequestContext(httpContext, routeData), HttpContext = httpContext};

            var viewModel = actionSectionMenuItem.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeTrue();
        }

        [TestMethod]
        public void When_the_controller_name_does_not_match_the_current_route_controller_value_then_the_section_menu_item_is_selected_method_should_return_false()
        {
            var actionSectionMenuItem = (ISectionMenuItem)new ActionSectionMenuItem(null, "MyAction", "MyController");
            var httpContext = new MockHttpContext(new MockHttpRequest(), new MockResponseBase());
            var routeData = new RouteData();
            routeData.Values.Add("controller", "MyDifferentController");
            routeData.Values.Add("action", "MyAction");

            var controllerContext = new ControllerContext
                                        {
                                            RequestContext = new RequestContext(httpContext, routeData),
                                            HttpContext = httpContext
                                        };

            var viewModel = actionSectionMenuItem.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeFalse();
        }

        [TestMethod]
        public void When_the_root_controller_and_action_match_the_route_data_the_is_selected_method_should_return_true()
        {
            var item = (ISectionMenuItem)new ActionSectionMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Pepper");
            routeData.Values.Add("action", "Eat");

            var viewContext = new ViewContext {RouteData = routeData};
            var controllerContext = GetControllerContext(null, new {parentActionViewContext = viewContext});

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeTrue();
        }

        [TestMethod]
        public void When_the_root_controller_three_levels_deep_and_action_match_the_route_data_the_is_selected_method_should_return_true()
        {
            var item = (ISectionMenuItem)new ActionSectionMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Pepper");
            routeData.Values.Add("action", "Eat");

            var viewContext = new ViewContext();
            viewContext.RouteData.DataTokens["parentActionViewContext"] = new ViewContext {RouteData = routeData};
            var controllerContext = GetControllerContext(null, new {parentActionViewContext = viewContext});

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeTrue();
        }

        private static ControllerContext GetControllerContext(object routeValues, object dataTokens)
        {
            var routeData = new RouteData();
            var values = new RouteValueDictionary(routeValues);
            var tokens = new RouteValueDictionary(dataTokens);

            foreach(var token in tokens)
                routeData.DataTokens.Add(token.Key, token.Value);

            foreach(var value in values)
                routeData.Values.Add(value.Key, value.Value);

            return new ControllerContext
                       {
                           RequestContext = new RequestContext {RouteData = routeData},
                           HttpContext = new MockHttpContext()
                       };
        }
    }
}