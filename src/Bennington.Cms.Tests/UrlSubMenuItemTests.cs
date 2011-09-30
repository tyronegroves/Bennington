using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class UrlSubMenuItemTests
    {
        [TestMethod]
        public void The_name_is_set_is_returned()
        {
            var item = new UrlSubMenuItem("My Test Name", null);

            var viewModel = item.GetViewModel(new ControllerContext());

            viewModel.Name.ShouldEqual("My Test Name");
        }

        [TestMethod]
        public void The_url_is_set_is_returned()
        {
            var item = new UrlSubMenuItem("My Test Name", "Url1");
            
            var viewModel = item.GetViewModel(new ControllerContext());

            viewModel.Url.ShouldEqual("Url1");
        }

        [TestMethod]
        public void The_is_selected_returns_false()
        {
            var item = new UrlSubMenuItem(null, null);

            var viewModel = item.GetViewModel(new ControllerContext());

            viewModel.Selected.ShouldBeFalse();
        }

        [TestMethod]
        public void The_is_visible_returns_false_if_no_function_is_defined()
        {
            var item = new UrlSubMenuItem(null, null);

            var viewModel = item.GetViewModel(new ControllerContext());

            viewModel.Visible.ShouldBeFalse();
        }

        [TestMethod]
        public void The_is_visible_returns_true_if_the_function_returns_true()
        {
            var item = new UrlSubMenuItem(null, null, context => true);

            var viewModel = item.GetViewModel(new ControllerContext());

            viewModel.Visible.ShouldBeTrue();
        }

        [TestMethod]
        public void The_is_visible_returns_false_if_the_function_returns_false()
        {
            var item = new UrlSubMenuItem(null, null, context => false);

            var viewModel = item.GetViewModel(new ControllerContext());

            viewModel.Visible.ShouldBeFalse();
        }
    }
}