using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.Controllers;
using Bennington.Cms.Models;
using Bennington.Cms.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class ListDetailControllerTests
    {
        [TestMethod]
        public void The_index_action_returns_the_index_view()
        {
            var controller = CreateListDetailController();

            var viewResults = (ViewResult)controller.Index();

            Assert.AreEqual("~/Views/ListDetail/Index.cshtml", viewResults.ViewName);
        }

        [TestMethod]
        public void A_grid_view_model_is_returned_when_index_is_called()
        {
            var controller = CreateListDetailController();

            var viewResults = (ViewResult)controller.Index();

            Assert.IsInstanceOfType(viewResults.Model, typeof(GridViewModel));
        }

        [TestMethod]
        public void The_grid_view_model_should_contain_the_items_returned_from_the_get_list_items()
        {
            var controller = CreateListDetailController();
            var fakeItem1 = new FakeListItem();
            var fakeItem2 = new FakeListItem();

            controller.GetListItemsToReturn = () => new List<FakeListItem> {fakeItem1, fakeItem2};

            dynamic viewResults = controller.Index();
            var items = viewResults.Model.Items;

            Assert.AreEqual(items.Count, 2);
            Assert.AreSame(items[0], fakeItem1);
            Assert.AreSame(items[1], fakeItem2);
        }

        [TestMethod]
        public void The_grid_view_model_title_should_be_the_name_of_the_controller()
        {
            var controller = CreateListDetailController();

            var title = controller.GetListTitle("FakeController");

            Assert.AreEqual("Fake", title);
        }

        [TestMethod]
        public void The_grid_view_model_title_should_be_the_name_of_the_controller_with_spaces_after_capital_letters()
        {
            var controller = CreateListDetailController();
            var title = controller.GetListTitle("NewsReleaseItemsController");

            Assert.AreEqual("News Release Items", title);
        }

        [TestMethod]
        public void The_type_name_is_passed_to_the_get_list_title_method_when_the_index_is_called()
        {
            var mockController = new Mock<ListDetailController<FakeForm, FakeListItem>> {CallBase = true};
            var controller = mockController.Object;
            var expectedTypeName = controller.GetType().Name;

            controller.Index();

            mockController.Verify(c => c.GetListTitle(expectedTypeName));
        }

        [TestMethod]
        public void The_grid_view_model_title_is_set_to_the_return_value_of_get_list_title_method_when_index_is_called()
        {
            var mockController = new Mock<ListDetailController<FakeForm, FakeListItem>> {CallBase = true};
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
            var controller = CreateListDetailController();
            var expectedGridColumns = new List<GridColumn>();
            controller.GridColumnsToReturn = expectedGridColumns;

            dynamic viewResult = controller.Index();

            Assert.AreSame(expectedGridColumns, viewResult.Model.Columns);
        }

        [TestMethod]
        public void The_grid_view_model_columns_contains_a_grid_column_item_for_each_property_on_the_item_when_index_is_called()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            var gridViewModel = (GridViewModel)viewResult.Model;

            Assert.AreEqual(2, viewResult.Model.Columns.Count);
            Assert.IsTrue(gridViewModel.Columns.Any(gc => gc.Name == "Property1"));
            Assert.IsTrue(gridViewModel.Columns.Any(gc => gc.Name == "Property2"));
        }

        [TestMethod]
        public void The_create_action_returns_the_create_view()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Create();

            Assert.AreEqual("~/Views/ListDetail/Create.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void The_create_action_returns_an_empty_form()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Create();

            Assert.IsInstanceOfType(viewResult.Model, typeof(FakeForm));
        }

        [TestMethod]
        public void The_create_action_redirects_to_the_manage_view_with_the_id()
        {
            var controller = CreateListDetailController();
            
            controller.RouteData.Values.Add("id", "someid");

            dynamic viewResult = controller.Create(new FakeForm());

            Assert.AreEqual("Manage", viewResult.RouteValues["action"]);
            Assert.AreEqual("someid", viewResult.RouteValues["id"]);
        }

        [TestMethod]
        public void The_create_action_should_create_the_item()
        {
            var controller = CreateListDetailController();
            var form = new FakeForm();

            controller.Create(form);

            Assert.IsTrue(controller.CreateItemWasCalled);
            Assert.AreSame(form, controller.CreateItemForm);
        }

        [TestMethod]
        public void The_create_action_does_not_call_create_item_when_the_model_is_invalid()
        {
            var controller = CreateListDetailController();
            var form = new FakeForm();

            controller.ModelState.AddModelError("Key", "Error");

            controller.Create(form);

            Assert.IsFalse(controller.CreateItemWasCalled);
        }

        [TestMethod]
        public void The_create_action_should_return_the_create_view_if_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Create(new FakeForm());

            Assert.AreEqual("~/Views/ListDetail/Create.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void The_create_action_should_return_the_form_if_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var form = new FakeForm();

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Create(form);

            Assert.AreSame(form, viewResult.Model);
        }

        [TestMethod]
        public void The_manage_action_should_return_the_manage_view()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Manage(new object());

            Assert.AreEqual("~/Views/ListDetail/Manage.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void The_manage_action_should_save_the_item()
        {
            var controller = CreateListDetailController();
            var form = new FakeForm();

            controller.Manage(form);

            Assert.IsTrue(controller.SaveItemWasCalled);
            Assert.AreSame(form, controller.SaveItemForm);
        }

        [TestMethod]
        public void The_manage_action_should_not_save_the_item_when_the_model_is_invalid()
        {
            var controller = CreateListDetailController();

            controller.ModelState.AddModelError("Key", "Error");

            controller.Manage(new FakeForm());

            Assert.IsFalse(controller.SaveItemWasCalled);
        }

        [TestMethod]
        public void The_manage_action_should_return_the_item()
        {
            var controller = CreateListDetailController();
            const int id = 1;
            var form = new FakeForm();

            controller.GetFormByIdItemToReturn = i => { return id.Equals(i) ? form : null; };

            dynamic viewResult = controller.Manage(id);

            Assert.AreEqual(form, viewResult.Model);
        }

        [TestMethod]
        public void The_manage_action_should_redirect_the_manage_view_with_the_id()
        {
            var controller = CreateListDetailController();

            controller.RouteData.Values.Add("id", "someid3");

            dynamic viewResult = controller.Manage(new FakeForm());

            Assert.AreEqual("Manage", viewResult.RouteValues["action"]);
            Assert.AreEqual("someid3", viewResult.RouteValues["id"]);
        }

        [TestMethod]
        public void The_manage_action_should_return_the_manage_view_when_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var form = new FakeForm();

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Manage(form);

            Assert.AreEqual("~/Views/ListDetail/Manage.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void The_manage_action_should_return_the_form_when_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var form = new FakeForm();

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Manage(form);

            Assert.AreSame(form, viewResult.Model);
        }

        public FakeController CreateListDetailController()
        {
            return new FakeController();
        }
    }

    public class FakeController : ListDetailController<FakeForm, FakeListItem>
    {
        public FakeController()
        {
            GetListItemsToReturn = () => new List<FakeListItem>();
            GetFormByIdItemToReturn = id => null;
            CreateItemWasCalled = false;
            SaveItemWasCalled = false;

            ControllerContext = new ControllerContext
                                    {
                                        RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
                                    };

        }

        public Func<IEnumerable<FakeListItem>> GetListItemsToReturn { get; set; }
        public List<GridColumn> GridColumnsToReturn { get; set; }
        public FakeForm CreateItemForm { get; set; }
        public Func<object, FakeForm> GetFormByIdItemToReturn { get; set; }

        public bool CreateItemWasCalled { get; set; }
        public bool SaveItemWasCalled { get; set; }
        public FakeForm SaveItemForm { get; set; }

        public override FakeForm GetFormById(object id)
        {
            return GetFormByIdItemToReturn(id);
        }

        public override IPagedList<FakeListItem> GetListItems(int pageIndex, int pageSize)
        {
            return new StaticPagedList<FakeListItem>(GetListItemsToReturn(), pageIndex, pageSize, 100);
        }

        protected override IEnumerable<GridColumn> GetColumns()
        {
            return GridColumnsToReturn ?? base.GetColumns();
        }

        public override void CreateItem(FakeForm form)
        {
            CreateItemForm = form;
            CreateItemWasCalled = true;
        }

        public override void SaveItem(FakeForm form)
        {
            SaveItemForm = form;
            SaveItemWasCalled = true;
        }
    }

    public class FakeListItem
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }

    public class FakeForm
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}