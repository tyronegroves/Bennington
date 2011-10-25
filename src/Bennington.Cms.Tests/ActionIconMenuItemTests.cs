using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models.MenuSystem;
using Bennington.Cms.Tests.Mocks;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof(ActionIconMenuItem))]
    public class When_the_view_model_is_retrieved_from_an_ActionIconMenuItem : ICleanupAfterEveryContextInAssembly
    {
        private static ActionIconMenuItem iconMenuItem;
        private static IconMenuItemViewModel viewModel;
        private static ControllerContext controllerContext;

        private Establish context = () =>
                                        {
                                            RouteTable.Routes.MapRoute(null, "{action}/{controller}/theend");
                                            controllerContext = new ControllerContext {HttpContext = new MockHttpContext()};
                                            iconMenuItem = new ActionIconMenuItem("MyName", "MyIconUrl", "Action", "MyController", new {dl = "myvalue"});
                                        };

        private Because of = () => viewModel = iconMenuItem.GetViewModel(controllerContext);

        private It should_return_the_name = () => viewModel.Name.ShouldEqual("MyName");

        private It should_return_the_url = () => viewModel.Url.ShouldEqual("/Action/MyController/theend?dl=myvalue");

        private It should_return_the_iconUrl = () => viewModel.IconUrl.ShouldEqual("MyIconUrl");

        public void AfterContextCleanup()
        {
            using(RouteTable.Routes.GetWriteLock())
                RouteTable.Routes.Clear();
        }
    }
}