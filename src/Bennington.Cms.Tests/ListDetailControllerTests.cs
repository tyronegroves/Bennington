using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Controllers;
using Bennington.Cms.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;

namespace Bennington.Cms.Tests
{
    [TestClass, Ignore]
    public class ListDetailControllerTests
    {
        [TestMethod]
        public void The_index_view_is_returned_when_index_is_called()
        {
            var controller = GetListDetailController();

            var viewResults = (ViewResult)controller.Index();

            Assert.AreEqual("~/Views/ListDetail/Index.cshtml", viewResults.ViewName);
        }

        [TestMethod]
        public void A_grid_view_model_is_returned_when_index_is_called()
        {
            var controller = GetListDetailController();

            var viewResults = (ViewResult)controller.Index();

            Assert.IsInstanceOfType(viewResults.Model, typeof(GridViewModel));
        }

        [TestMethod]
        public void The_grid_view_model_should_contain_the_items_returned_from_the_get_list_items()
        {
            var controller = GetListDetailController();
            var expectedItems = new List<FakeListItem>();

            controller.GetListItemsToReturn = () => expectedItems;

            dynamic viewResults = controller.Index();

            Assert.AreSame(viewResults.Model.Items, expectedItems);
        }

        [TestMethod]
        public void The_grid_view_model_title_should_be_the_name_of_the_controller()
        {
            var controller = GetListDetailController();

            var title = controller.GetListTitle("FakeController");

            Assert.AreEqual("Fake", title);
        }

        [TestMethod]
        public void The_grid_view_model_title_should_be_the_name_of_the_controller_with_spaces_after_capital_letters()
        {
            var controller = GetListDetailController();
            var title = controller.GetListTitle("NewsReleaseItemsController");

            Assert.AreEqual("News Release Items", title);
        }

        [TestMethod]
        public void The_type_name_is_passed_to_the_get_list_title_method_when_the_index_is_called()
        {
            var mockController = new Mock<ListDetailController<FakeListItem>> {CallBase = true};
            var controller = mockController.Object;
            var expectedTypeName = controller.GetType().Name;

            controller.Index();

            mockController.Verify(c => c.GetListTitle(expectedTypeName));
        }

        [TestMethod]
        public void The_grid_view_model_title_is_set_to_the_return_value_of_get_list_title_method_when_index_is_called()
        {
            var mockController = new Mock<ListDetailController<FakeListItem>> {CallBase = true};
            var controller = mockController.Object;
            var expectedTypeName = controller.GetType().Name;

            mockController
                .Setup(c => c.GetListTitle(expectedTypeName))
                .Returns("My Test Title");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("My Test Title", viewResult.Model.Title);
        }

        [TestMethod]
        public void The_grid_view_model_columns_is_set_to_the_return_value_of_get_columns_when_index_is_called()
        {
            var controller = GetListDetailController();
            var expectedGridColumns = new List<GridColumn>();
            controller.GridColumnsToReturn = expectedGridColumns;

            dynamic viewResult = controller.Index();

            Assert.AreSame(expectedGridColumns, viewResult.Model.Columns);
        }

        [TestMethod]
        public void The_grid_view_model_columns_contains_a_grid_column_item_for_each_property_on_the_item_when_index_is_called()
        {
            var controller = GetListDetailController();

            dynamic viewResult = controller.Index();

            var gridViewModel = (GridViewModel)viewResult.Model;

            Assert.AreEqual(2, viewResult.Model.Columns.Count);
            Assert.IsTrue(gridViewModel.Columns.Any(gc => gc.Name == "Property1"));
            Assert.IsTrue(gridViewModel.Columns.Any(gc => gc.Name == "Property2"));
        }

        public FakeController GetListDetailController()
        {
            return new FakeController();
        }
    }

    public class FakeController : ListDetailController<FakeListItem>
    {
        public FakeController()
        {
            GetListItemsToReturn = () => null;
        }

        public Func<IEnumerable<FakeListItem>> GetListItemsToReturn { get; set; }
        public List<GridColumn> GridColumnsToReturn { get; set; }

        protected override IPagedList<object> GetListItems()
        {
            return new StaticPagedList<object>(GetListItemsToReturn(), 0, 0, 100);
        }

        protected override IEnumerable<GridColumn> GetColumns()
        {
            return GridColumnsToReturn ?? base.GetColumns();
        }
    }

    public class FakeListItem
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}