using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof(UrlIconMenuItem))]
    public class When_the_view_model_is_retrieved_from_a_UrlIconMenuItem
    {
        private Establish context = () => { iconMenuItem = new UrlIconMenuItem("Name1", "http://help.com", "http://icon.ico"); };

        private Because of = () => viewModel = iconMenuItem.GetViewModel(new ControllerContext());

        private It should_have_the_name_set = () => viewModel.Name.ShouldEqual("Name1");

        private It should_have_the_url_set = () => viewModel.Url.ShouldEqual("http://help.com");

        private It should_have_the_icon_url_set = () => viewModel.IconUrl.ShouldEqual("http://icon.ico");

        private static UrlIconMenuItem iconMenuItem;
        private static IconMenuItemViewModel viewModel;
    }
}