using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Tests.Mocks;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class ActionSubMenuItemTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            using(RouteTable.Routes.GetWriteLock())
                RouteTable.Routes.Clear();
        }

        [TestMethod]
        public void When_the_name_is_set_it_is_returned_from_get_name()
        {
            var item = new ActionSubMenuItem("My Menu Item", null, null, null);

            var viewModel = item.GetViewModel(GetControllerContext(new {controller = "None"}, null));

            viewModel.Name.ShouldEqual("My Menu Item");
        }

        [TestMethod]
        public void When_get_url_is_called_the_url_for_the_route_data_is_returned()
        {
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var controllerContext = GetControllerContext(new {controller = "None"}, null);

            RouteTable.Routes.MapRoute(null, "something/{action}/{controller}");

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Url.ShouldEqual("/something/Eat/Pepper?pid=tyrone&ln=groves");
        }

        [TestMethod]
        public void When_the_controller_and_action_match_the_route_data_the_is_selected_method_should_return_true()
        {
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var controllerContext = GetControllerContext(new {controller = "Pepper", action = "Eat"}, null);

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeTrue();
        }

        [TestMethod]
        public void When_the_controller_does_not_match_the_route_data_the_is_selected_method_should_return_false()
        {
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var controllerContext = GetControllerContext(new {controller = "Fruit", action = "Eat"}, null);

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeFalse();
        }

        [TestMethod]
        public void When_the_action_does_not_match_the_route_data_the_is_selected_method_should_return_false()
        {
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var controllerContext = GetControllerContext(new {controller = "Pepper", action = "Cut"}, null);

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeFalse();
        }

        [TestMethod]
        public void When_the_root_controller_and_action_match_the_route_data_the_is_selected_method_should_return_true()
        {
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
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
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Pepper");
            routeData.Values.Add("action", "Eat");

            var viewContext = new ViewContext();
            viewContext.RouteData.DataTokens["parentActionViewContext"] = new ViewContext {RouteData = routeData};
            var controllerContext = GetControllerContext(null, new {parentActionViewContext = viewContext});

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Selected.ShouldBeTrue();
        }

        [TestMethod]
        public void When_the_controller_matches_the_route_data_the_is_visible_method_should_return_true()
        {
            var item = new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var controllerContext = GetControllerContext(new {controller = "Pepper", action = "Eat"}, null);

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Visible.ShouldBeTrue();
        }

        [TestMethod]
        public void When_the_controller_does_not_match_the_route_data_the_is_visible_method_should_return_false()
        {
            var item = (ISubMenuItem)new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var controllerContext = GetControllerContext(new {controller = "Fruit", action = "Eat"}, null);

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Visible.ShouldBeFalse();
        }

        [TestMethod]
        public void When_the_root_controller_match_the_route_data_the_is_visible_method_should_return_true()
        {
            var item = (ISubMenuItem)new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Pepper");
            routeData.Values.Add("action", "Cut");

            var viewContext = new ViewContext {RouteData = routeData};
            var controllerContext = GetControllerContext(null, new {parentActionViewContext = viewContext});

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Visible.ShouldBeTrue();
        }

        [TestMethod]
        public void When_the_root_controller_three_levels_deep_match_the_route_data_the_is_visible_method_should_return_true()
        {
            var item = (ISubMenuItem)new ActionSubMenuItem("My Item", "Eat", "Pepper", new {pid = "tyrone", ln = "groves"});
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Pepper");
            routeData.Values.Add("action", "Steal");

            var viewContext = new ViewContext();
            viewContext.RouteData.DataTokens["parentActionViewContext"] = new ViewContext {RouteData = routeData};
            var controllerContext = GetControllerContext(null, new {parentActionViewContext = viewContext});

            var viewModel = item.GetViewModel(controllerContext);

            viewModel.Visible.ShouldBeTrue();
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

            var mockHttpContext = new MockHttpContext();
            return new ControllerContext
                       {
                           RequestContext = new RequestContext(mockHttpContext, routeData),
                           HttpContext = mockHttpContext
                       };
        }
    }
}