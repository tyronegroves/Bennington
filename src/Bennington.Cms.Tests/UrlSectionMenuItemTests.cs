using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;
using Bennington.Cms.Models.MenuSystem;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof(UrlSectionMenuItem))]
    public class When_a_view_model_is_retrieved_from_an_url_section_menu_item
    {
        private Establish context = () => { sectionMenuItem = new UrlSectionMenuItem("My Sub Name", "http://test.com"); };

        private Because of = () => { viewModel = sectionMenuItem.GetViewModel(new ControllerContext()); };

        private It should_have_the_name_set = () => viewModel.Name.ShouldEqual("My Sub Name");

        private It should_have_the_url_set = () => viewModel.Url.ShouldEqual("http://test.com");

        private It should_have_the_selected_set = () => viewModel.Selected.ShouldBeFalse();

        private static UrlSectionMenuItem sectionMenuItem;
        private static SectionMenuItemViewModel viewModel;
    }
}