using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMoq;
using Bennington.Cms.Attributes;
using Bennington.Cms.Controllers;
using Bennington.Cms.Tests.Mocks;
using Bennington.Core.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class ListDetailControllerTests
    {
        private AutoMoqer mocker;
        private NameValueCollection queryString;
        private NameValueCollection form;
        private ListColumns listColumns;
        private MockHttpContext httpContext;
        private MockViewEngine viewEngine;
        private RouteData routeData;
        private MockModelValidatorProvider modelValidatorProvider;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
            form = new NameValueCollection();
            queryString = new NameValueCollection();
            listColumns = new ListColumns();
            viewEngine = new MockViewEngine();
            routeData = new RouteData();
            modelValidatorProvider = new MockModelValidatorProvider();

            mocker.GetMock<IListColumnProvider>()
                .Setup(provider => provider.GetColumnsForType(typeof(RegionalSalesReportsListItem), It.IsAny<ControllerContext>()))
                .Returns(listColumns);

            ModelValidatorProviders.Providers
                .ReplaceWith<MockModelValidatorProvider>(modelValidatorProvider);

            ValueProviderFactories.Factories
                .ReplaceWith<HttpFileCollectionValueProviderFactory>(ctx => ctx.HttpContext.Request.Form)
                .ReplaceWith<FormValueProviderFactory>(ctx => ctx.HttpContext.Request.Form)
                .ReplaceWith<QueryStringValueProviderFactory>(ctx => ctx.HttpContext.Request.QueryString);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(viewEngine);

            httpContext = new MockHttpContext();
            ((MockHttpRequest)httpContext.Request).SetQueryString(queryString);
            ((MockHttpRequest)httpContext.Request).SetForm(form);
            routeData = new RouteData();
            routeData.Values.Add("action", "Index");

            RouteTable.Routes.MapRoute(null, "{action}/{controller}");
        }

        [TestCleanup]
        public void Cleanup()
        {
            using(RouteTable.Routes.GetWriteLock())
                RouteTable.Routes.Clear();
        }

        [TestMethod]
        public void Index_action_returns_default_list_view_if_no_index_view_is_found()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("~/Views/ListDetail/List.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void Index_action_returns_index_view_if_an_index_view_is_found()
        {
            var controller = CreateListDetailController();
            var view = new MockView();

            viewEngine.MakeViewExists("List", view);

            dynamic viewResult = controller.Index();

            Assert.AreSame(view, viewResult.View);
        }

        [TestMethod]
        public void Index_action_returns_list_view_model_if_an_index_view_is_found()
        {
            var controller = CreateListDetailController();

            mocker.GetMock<IViewEngine>()
                .Setup(ve => ve.FindView(controller.ControllerContext, "Index", null, It.IsAny<bool>()))
                .Returns(() => new ViewEngineResult(mocker.GetMock<IView>().Object, mocker.GetMock<IViewEngine>().Object));

            dynamic viewResult = controller.Index();

            Assert.IsInstanceOfType(viewResult.Model, typeof(ListViewModel));
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.IsInstanceOfType(viewResult.Model, typeof(ListViewModel));
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_page_index_set()
        {
            var controller = CreateListDetailController();
            queryString.Add("pageindex", "3");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.PageIndex);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_page_size()
        {
            var controller = CreateListDetailController();
            queryString.Add("pagesize", "40");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(40, viewResult.Model.PageSize);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_default_page_size()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual(25, viewResult.Model.PageSize);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_sort_by_set()
        {
            var controller = CreateListDetailController();
            queryString.Add("sortby", "mycolumn");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("mycolumn", viewResult.Model.SortBy);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_sort_direction_set_to_ascending()
        {
            var controller = CreateListDetailController();
            queryString.Add("sortdirection", "ascending");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(SortDirection.Ascending, viewResult.Model.SortDirection);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_sort_direction_set_to_descending()
        {
            var controller = CreateListDetailController();
            queryString.Add("sortdirection", "descending");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(SortDirection.Descending, viewResult.Model.SortDirection);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_sort_direction_set_to_ascending_by_default()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual(SortDirection.Ascending, viewResult.Model.SortDirection);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_title_set()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Regional Sales Reports", viewResult.Model.Title);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_search_form_value_set()
        {
            var controller = CreateListDetailController();

            form.Add("SearchValue", "Southeast");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Southeast", viewResult.Model.SearchValue[0]);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_search_form_column_set()
        {
            var controller = CreateListDetailController();

            form.Add("SearchBy", "Region");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Region", viewResult.Model.SearchBy);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_search_form_with_the_search_url_set()
        {
            var controller = CreateListDetailController();

            queryString.Add("sortby", "mycolumn");
            queryString.Add("sortdirection", "descending");
            queryString.Add("searchby", "Region2");
            queryString.Add("searchvalue", "Topeka");

            RouteTable.Routes.MapRoute("test", "{controller}/{action}", new {controller = "MyController"});

            dynamic viewResult = controller.Index();

            Assert.AreEqual("/MyController/Index?sortby=mycolumn&sortdirection=Descending".ToLower(), viewResult.Model.SearchUrl.ToLower());
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_columns_set()
        {
            var controller = CreateListDetailController();
            var columns = new ListColumns();

            mocker.GetMock<IListColumnProvider>()
                .Setup(provider => provider.GetColumnsForType(typeof(RegionalSalesReportsListItem), It.IsAny<ControllerContext>()))
                .Returns(columns);

            dynamic viewResult = controller.Index();

            Assert.AreSame(columns, viewResult.Model.Columns);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_list_header_columns_set()
        {
            var controller = CreateListDetailController();
            var columns = new ListColumns();

            mocker.GetMock<IListColumnProvider>()
                .Setup(provider => provider.GetColumnsForType(typeof(RegionalSalesReportsListItem), It.IsAny<ControllerContext>()))
                .Returns(columns);

            dynamic viewResult = controller.Index();

            Assert.AreSame(columns, viewResult.Model.Columns);
        }

        [TestMethod]
        public void Index_action_returns_a_list_view_model_with_the_list_items_set()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem();
            var listItem2 = new RegionalSalesReportsListItem();
            var expectedItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2};

            controller.ListItems = expectedItems.AsQueryable();

            dynamic viewResult = controller.Index();

            Assert.AreEqual(2, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem2, viewResult.Model.Items[1]);
        }

        [TestMethod]
        public void Index_action_passes_the_sortby_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            queryString.Add("sortby", "somevalue");

            controller.Index();

            Assert.AreEqual("somevalue", controller.SortBy);
        }

        [TestMethod]
        public void Index_action_passes_the_sort_direction_desc_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            queryString.Add("sortdirection", "descending");

            controller.Index();

            Assert.AreEqual(SortDirection.Descending, controller.SortDirection);
        }

        [TestMethod]
        public void Index_action_passes_the_sort_direction_asc_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            queryString.Add("sortorder", "asc");

            controller.Index();

            Assert.AreEqual(SortDirection.Ascending, controller.SortDirection);
        }

        [TestMethod]
        public void Index_action_passes_the_page_index_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            queryString.Add("pageindex", "5");

            controller.Index();

            Assert.AreEqual(5, controller.PageIndex);
        }

        [TestMethod]
        public void Index_action_passes_the_page_size_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            queryString.Add("pagesize", "67");

            controller.Index();

            Assert.AreEqual(67, controller.PageSize);
        }

        [TestMethod]
        public void Index_action_passes_the_search_by_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            form.Add("SearchBy", "Region");

            controller.Index();

            Assert.AreEqual("Region", controller.SearchBy);
        }

        [TestMethod]
        public void Index_action_passes_the_search_by_to_get_paged_list_items_method_if_its_in_the_query_string()
        {
            var controller = CreateListDetailController();

            queryString.Add("searchby", "Region2");

            controller.Index();

            Assert.AreEqual("Region2", controller.SearchBy);
        }

        [TestMethod]
        public void Index_action_passes_the_search_value_to_get_paged_list_items_method()
        {
            var controller = CreateListDetailController();

            form.Add("SearchValue", "Houston");

            controller.Index();

            Assert.AreEqual("Houston", controller.SearchValue[0]);
        }

        [TestMethod]
        public void Index_action_passes_the_search_value_to_get_paged_list_items_method_if_its_in_the_query_string()
        {
            var controller = CreateListDetailController();

            queryString.Add("searchvalue", "Topeka");

            controller.Index();

            Assert.AreEqual("Topeka", controller.SearchValue[0]);
        }

        [TestMethod]
        public void Create_action_returns_the_default_create_view()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Create();

            Assert.AreEqual("~/Views/ListDetail/Create.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void Create_action_returns_the_create_view_if_it_exists()
        {
            var controller = CreateListDetailController();
            var view = new MockView();

            viewEngine.MakeViewExists("Create", view);

            dynamic viewResult = controller.Create();

            Assert.AreSame(view, viewResult.View);
        }

        [TestMethod]
        public void Create_action_returns_a_new_form_when_the_default_manage_view_is_returned()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.CreatedForm = exceptedForm;

            dynamic viewResult = controller.Create();

            Assert.AreSame(exceptedForm, viewResult.Model);
        }

        [TestMethod]
        public void Create_action_returns_a_new_form_when_the_manage_view_is_returned()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            mocker.GetMock<IViewEngine>()
                .Setup(ve => ve.FindView(controller.ControllerContext, "Create", null, It.IsAny<bool>()))
                .Returns(() => new ViewEngineResult(mocker.GetMock<IView>().Object, mocker.GetMock<IViewEngine>().Object));

            controller.CreatedForm = exceptedForm;

            dynamic viewResult = controller.Create();

            Assert.AreSame(exceptedForm, viewResult.Model);
        }

        [TestMethod]
        public void Create_action_returns_to_the_default_create_view_if_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Create(new RegionalSalesReportsForm());

            Assert.AreEqual("~/Views/ListDetail/Create.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void Create_action_returns_the_create_view_if_it_exists_and_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var view = new MockView();

            viewEngine.MakeViewExists("Create", view);

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Create(new RegionalSalesReportsForm());

            Assert.AreSame(view, viewResult.View);
        }

        [TestMethod]
        public void Create_action_returns_to_the_form_if_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Create(exceptedForm);

            Assert.AreSame(exceptedForm, viewResult.Model);
        }

        [TestMethod]
        public void Create_action_returns_form_if_the_create_view_exists_and_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();
            var view = mocker.GetMock<IView>().Object;

            mocker.GetMock<IViewEngine>()
                .Setup(ve => ve.FindView(controller.ControllerContext, "Create", null, It.IsAny<bool>()))
                .Returns(() => new ViewEngineResult(view, mocker.GetMock<IViewEngine>().Object));

            controller.ModelState.AddModelError("Key", "Error");

            dynamic viewResult = controller.Create(exceptedForm);

            Assert.AreSame(exceptedForm, viewResult.Model);
        }

        [TestMethod]
        public void Create_action_redirects_to_the_manage_action()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Create(new RegionalSalesReportsForm());

            Assert.AreEqual("Manage", viewResult.RouteValues["action"]);
        }

        [TestMethod]
        public void The_no_id_is_returned_when_the_model_is_null()
        {
            var controller = CreateListDetailController();

            dynamic id = controller.GetId(null);

            Assert.IsNull(id);
        }

        [TestMethod]
        public void The_no_id_is_returned_when_the_model_does_not_have_an_id()
        {
            var controller = CreateListDetailController();

            dynamic id = controller.GetId(new ModelWithNoId());

            Assert.IsNull(id);
        }

        [TestMethod]
        public void The_no_id_is_returned_when_the_model_has_no_identifier_attribute()
        {
            var controller = CreateListDetailController();

            dynamic id = controller.GetId(new ModelWithNoIdAttribute {Id = "myvalue1"});

            Assert.IsNull(id);
        }

        [TestMethod]
        public void The_id_property_is_returned_when_the_model_has_an_id_property()
        {
            var controller = CreateListDetailController();

            dynamic id = controller.GetId(new ModelWithId {Id = "myvalue"});

            Assert.AreEqual("myvalue", id);
        }

        [TestMethod]
        public void The_myid_property_is_returned_when_the_model_has_an_myid_property_with_the_identifier_attribute_on_it()
        {
            var controller = CreateListDetailController();

            dynamic id = controller.GetId(new ModelWithMyId {MyId = "myvalue"});

            Assert.AreEqual("myvalue", id);
        }

        [TestMethod]
        public void The_form_is_inserted_when_the_create_method_is_called()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.Create(exceptedForm);

            Assert.AreSame(exceptedForm, controller.InsertedForm);
        }

        [TestMethod]
        public void The_form_is_not_inserted_when_the_create_method_is_called_and_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.ModelState.AddModelError("Key", "Error");

            controller.Create(exceptedForm);

            Assert.IsNull(controller.InsertedForm);
        }

        [TestMethod]
        public void Create_action_redirects_to_the_manage_action_with_the_id()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Create(new RegionalSalesReportsForm {Id = 56});

            Assert.AreEqual("Manage", viewResult.RouteValues["action"]);
            Assert.AreEqual(56, viewResult.RouteValues["id"]);
        }

        [TestMethod]
        public void Manage_action_returns_the_default_manage_view()
        {
            var controller = CreateListDetailController();

            queryString.Add("id", "5");

            dynamic viewResult = controller.Manage();

            Assert.AreEqual("~/Views/ListDetail/Manage.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void Manage_action_returns_the_form()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            queryString.Add("id", "2");

            controller.GetFormByIdForm = exceptedForm;

            dynamic viewResult = controller.Manage();

            Assert.AreSame(exceptedForm, viewResult.Model);
            Assert.AreEqual(2, controller.GetFormByIdId);
        }

        [TestMethod]
        public void Manage_action_returns_the_default_manage_view_if_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.ModelState.AddModelError("Key", "Error");
            controller.GetFormByIdForm = exceptedForm;

            dynamic viewResult = controller.Manage(exceptedForm);

            Assert.AreEqual("~/Views/ListDetail/Manage.cshtml", viewResult.ViewName);
        }

        [TestMethod]
        public void Manage_action_returns_the_manage_view_if_the_manage_view_exists_and_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();
            var view = new MockView();

            viewEngine.MakeViewExists("Manage", view);

            controller.ModelState.AddModelError("Key", "Error");
            controller.GetFormByIdForm = exceptedForm;

            dynamic viewResult = controller.Manage(exceptedForm);

            Assert.AreSame(view, viewResult.View);
        }

        [TestMethod]
        public void Manage_action_returns_the_form_if_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.ModelState.AddModelError("Key", "Error");
            controller.GetFormByIdForm = exceptedForm;

            dynamic viewResult = controller.Manage(exceptedForm);

            Assert.AreEqual(exceptedForm, viewResult.Model);
        }

        [TestMethod]
        public void Manage_action_returns_the_form_if_the_manage_view_exists_and_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            mocker.GetMock<IViewEngine>()
                .Setup(ve => ve.FindView(controller.ControllerContext, "Manage", null, It.IsAny<bool>()))
                .Returns(() => new ViewEngineResult(mocker.GetMock<IView>().Object, mocker.GetMock<IViewEngine>().Object));

            controller.ModelState.AddModelError("Key", "Error");
            controller.GetFormByIdForm = exceptedForm;

            dynamic viewResult = controller.Manage(exceptedForm);

            Assert.AreSame(exceptedForm, viewResult.Model);
        }

        [TestMethod]
        public void Manage_action_returns_the_manage_view_if_it_exists()
        {
            var controller = CreateListDetailController();
            var view = new MockView();

            queryString.Add("id", "3");

            viewEngine.MakeViewExists("Manage", view);

            dynamic viewResult = controller.Manage();

            Assert.AreSame(view, viewResult.View);
        }

        [TestMethod]
        public void Manage_action_returns_the_form_if_the_manage_view_exists()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            mocker.GetMock<IViewEngine>()
                .Setup(ve => ve.FindView(controller.ControllerContext, "Manage", null, It.IsAny<bool>()))
                .Returns(() => new ViewEngineResult(mocker.GetMock<IView>().Object, mocker.GetMock<IViewEngine>().Object));

            controller.GetFormByIdForm = exceptedForm;

            queryString.Add("id", "3");

            dynamic viewResult = controller.Manage();

            Assert.AreSame(exceptedForm, viewResult.Model);
            Assert.AreEqual(3, controller.GetFormByIdId);
        }

        [TestMethod]
        public void Manage_action_redirects_to_the_manage_action()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Manage(new RegionalSalesReportsForm());

            Assert.AreEqual("Manage", viewResult.RouteValues["action"]);
        }

        [TestMethod]
        public void Manage_action_redirects_to_the_manage_action_with_the_id()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Manage(new RegionalSalesReportsForm {Id = 765});

            Assert.AreEqual("Manage", viewResult.RouteValues["action"]);
            Assert.AreEqual(765, viewResult.RouteValues["id"]);
        }

        [TestMethod]
        public void Manage_action_updates_the_form()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.Manage(exceptedForm);

            Assert.AreSame(exceptedForm, controller.UpdatedForm);
        }

        [TestMethod]
        public void Manage_action_does_not_updates_the_form_when_the_model_state_is_invalid()
        {
            var controller = CreateListDetailController();
            var exceptedForm = new RegionalSalesReportsForm();

            controller.ModelState.AddModelError("Key", "Error");

            controller.Manage(exceptedForm);

            Assert.IsNull(controller.UpdatedForm);
        }

        [TestMethod]
        public void Delete_action_redirects_to_index_action()
        {
            var controller = CreateListDetailController();

            controller.Delete("my id");

            Assert.AreEqual("my id", controller.DeleteItemId);
        }

        [TestMethod]
        public void Index_action_sets_the_title_view_name_to_the_default_title_view_name()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("~/Views/ListDetail/Title.cshtml", viewResult.Model.TitleViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_title_view_name_if_a_title_view_is_found()
        {
            var controller = CreateListDetailController();

            viewEngine.MakeViewExists("Title");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Title", viewResult.Model.TitleViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_search_form_view_name_to_the_default_search_form_view_name()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("~/Views/ListDetail/SearchForm.cshtml", viewResult.Model.SearchViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_search_form_view_name_if_a_search_form_view_is_found()
        {
            var controller = CreateListDetailController();

            viewEngine.MakeViewExists("SearchForm");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("SearchForm", viewResult.Model.SearchViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_buttons_view_name_to_the_default_buttons_view_name()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("~/Views/ListDetail/Buttons.cshtml", viewResult.Model.ButtonsViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_buttons_view_name_if_a_buttons_view_is_found()
        {
            var controller = CreateListDetailController();

            viewEngine.MakeViewExists("Buttons");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Buttons", viewResult.Model.ButtonsViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_pager_view_name_to_the_default_buttons_view_name()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("~/Views/ListDetail/Pager.cshtml", viewResult.Model.PagerViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_pager_view_name_if_a_pager_view_is_found()
        {
            var controller = CreateListDetailController();

            viewEngine.MakeViewExists("Pager");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Pager", viewResult.Model.PagerViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_rows_view_name_to_the_default_rows_view_name()
        {
            var controller = CreateListDetailController();

            dynamic viewResult = controller.Index();

            Assert.AreEqual("~/Views/ListDetail/Rows.cshtml", viewResult.Model.RowsViewName);
        }

        [TestMethod]
        public void Index_action_sets_the_rows_view_name_if_a_rows_view_is_found()
        {
            var controller = CreateListDetailController();

            viewEngine.MakeViewExists("Rows");

            dynamic viewResult = controller.Index();

            Assert.AreEqual("Rows", viewResult.Model.RowsViewName);
        }

        [TestMethod]
        public void Index_action_returns_the_items_sorted_by_the_sort_by_property()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Southwest"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Northeast"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            listColumns.Add(new ListColumn {Name = "Region"});

            queryString.Add("sortby", "region");

            controller.ListItems = listItems.AsQueryable();

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.Items.Count);
            Assert.AreSame(listItem2, viewResult.Model.Items[0]);
            Assert.AreSame(listItem3, viewResult.Model.Items[1]);
            Assert.AreSame(listItem1, viewResult.Model.Items[2]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_unsorted_when_sorted_by_a_property_that_does_not_exists()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Southwest"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Northeast"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            queryString.Add("sortby", "reion");

            controller.ListItems = listItems.AsQueryable();

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem2, viewResult.Model.Items[1]);
            Assert.AreSame(listItem3, viewResult.Model.Items[2]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_sorted_descending_when_sort_by_and_sort_direction_exists()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Southwest"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Northeast"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "Region"});
            queryString.Add("sortby", "region");
            queryString.Add("sortdirection", "descending");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem3, viewResult.Model.Items[1]);
            Assert.AreSame(listItem2, viewResult.Model.Items[2]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_sorted_descending_when_sort_by_and_sort_ascending_exists()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Southwest"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Northeast"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "Region"});
            queryString.Add("sortby", "region");
            queryString.Add("sortdirection", "ascending");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.Items.Count);
            Assert.AreSame(listItem2, viewResult.Model.Items[0]);
            Assert.AreSame(listItem3, viewResult.Model.Items[1]);
            Assert.AreSame(listItem1, viewResult.Model.Items[2]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_that_match_the_search_criteria_exactly()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Southwest"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Northwest"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "Region", Type = typeof(string)});
            form.Add("searchby", "region");
            form.Add("searchvalue", "Midwest");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(1, viewResult.Model.Items.Count);
            Assert.AreSame(listItem2, viewResult.Model.Items[0]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_that_match_the_search_criteria_the_beginning()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Mile High"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Mid-Alantic"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "Region", Type = typeof(string)});
            form.Add("searchby", "region");
            form.Add("searchvalue", "Mid");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(2, viewResult.Model.Items.Count);
            Assert.AreSame(listItem2, viewResult.Model.Items[0]);
            Assert.AreSame(listItem3, viewResult.Model.Items[1]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_that_match_the_search_criteria_when_no_value_is_provided()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Mile High"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Mid-Alantic"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "Region"});
            form.Add("searchby", "region");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem2, viewResult.Model.Items[1]);
            Assert.AreSame(listItem3, viewResult.Model.Items[2]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_that_match_the_search_criteria_when_search_value_is_empty_string()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Mile High"};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest"};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Mid-Alantic"};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "Region", Type = typeof(string)});
            form.Add("searchby", "region");
            form.Add("searchvalue", string.Empty);

            dynamic viewResult = controller.Index();

            Assert.AreEqual(3, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem2, viewResult.Model.Items[1]);
            Assert.AreSame(listItem3, viewResult.Model.Items[2]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_paged()
        {
            var controller = CreateListDetailController();
            var listItems = new List<RegionalSalesReportsListItem>();
            listItems.AddRange(Enumerable.Repeat(new RegionalSalesReportsListItem(), 1000));

            controller.ListItems = listItems.AsQueryable();

            queryString.Add("pageindex", "20");
            queryString.Add("pagesize", "12");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(12, viewResult.Model.Items.PageSize);
            Assert.AreEqual(20, viewResult.Model.Items.PageIndex);
            Assert.AreEqual(1000, viewResult.Model.Items.TotalItemCount);
        }

        [TestMethod]
        public void Index_action_returns_no_error_when_sort_direction_is_invalid()
        {
            var controller = CreateListDetailController();

            queryString.Add("sortdirection", "dd");

            controller.Index();
        }

        [TestMethod]
        public void Index_action_returns_the_items_that_match_the_items_the_have_a_date_value_after_search_value()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Mile High", ReportRunDate = DateTime.Parse("10/2/2011")};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest", ReportRunDate = DateTime.Parse("10/3/2011")};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Mid-Alantic", ReportRunDate = DateTime.Parse("9/4/2011")};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "ReportRunDate", Type = typeof(DateTime)});
            form.Add("searchby", "reportrundate");
            form.Add("searchvalue", "10/2/2011");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(2, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem2, viewResult.Model.Items[1]);
        }

        [TestMethod]
        public void Index_action_returns_the_items_that_match_the_items_the_have_a_nullable_date_time_value_after_search_value()
        {
            var controller = CreateListDetailController();
            var listItem1 = new RegionalSalesReportsListItem {Region = "Mile High", ReportRunDate = DateTime.Parse("10/2/2011")};
            var listItem2 = new RegionalSalesReportsListItem {Region = "Midwest", ReportRunDate = DateTime.Parse("10/3/2011")};
            var listItem3 = new RegionalSalesReportsListItem {Region = "Mid-Alantic", ReportRunDate = DateTime.Parse("9/4/2011")};
            var listItems = new List<RegionalSalesReportsListItem> {listItem1, listItem2, listItem3};

            controller.ListItems = listItems.AsQueryable();

            listColumns.Add(new ListColumn {Name = "ReportRunDate", Type = typeof(DateTime?)});
            form.Add("searchby", "reportrundate");
            form.Add("searchvalue", "10/2/2011");

            dynamic viewResult = controller.Index();

            Assert.AreEqual(2, viewResult.Model.Items.Count);
            Assert.AreSame(listItem1, viewResult.Model.Items[0]);
            Assert.AreSame(listItem2, viewResult.Model.Items[1]);
        }

        [TestMethod]
        public void Index_action_validates_the_model()
        {
            var controller = CreateListDetailController();

            controller.Index();

            Assert.IsTrue(modelValidatorProvider.TypeWasValidated(typeof(ListViewModel)));
        }

        private RegionalSalesReportsController CreateListDetailController()
        {
            var controller = mocker.Resolve<RegionalSalesReportsController>();
            controller.ControllerContext = new ControllerContext(httpContext, routeData, controller);
            controller.Url = new UrlHelper(controller.ControllerContext.RequestContext);

            return controller;
        }
    }

    public class RegionalSalesReportsController : ListDetailController<RegionalSalesReportsListItem, RegionalSalesReportsForm>
    {
        public RegionalSalesReportsController(IListColumnProvider listColumnProvider)
        {
        }

        public string Subtitle { get; set; }
        public IQueryable<RegionalSalesReportsListItem> ListItems { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SearchBy { get; set; }
        public dynamic SearchValue { get; set; }
        public RegionalSalesReportsForm CreatedForm { get; set; }
        public RegionalSalesReportsForm InsertedForm { get; set; }
        public RegionalSalesReportsForm GetFormByIdForm { get; set; }
        public object GetFormByIdId { get; set; }
        public RegionalSalesReportsForm UpdatedForm { get; set; }
        public object DeleteItemId { get; set; }
        public List<RegionalSalesReportsListItem> UnpagedListItems { get; set; }
        public ActionResult IndexViewResult { get; set; }

        public override ActionResult Index()
        {
            return IndexViewResult = base.Index();
        }

        public override void DeleteItem(object id)
        {
            DeleteItemId = id;
        }

        public override void UpdateForm(RegionalSalesReportsForm form)
        {
            UpdatedForm = form;
        }

        protected override RegionalSalesReportsForm CreateForm()
        {
            return CreatedForm;
        }

        public override void InsertForm(RegionalSalesReportsForm form)
        {
            InsertedForm = form;
        }

        public override RegionalSalesReportsForm GetFormById(object id)
        {
            GetFormByIdId = id;
            return GetFormByIdForm;
        }

        public new object GetId(object model)
        {
            return base.GetId(model);
        }

        protected override IQueryable<RegionalSalesReportsListItem> GetListItems(ListViewModel listViewModel)
        {
            SortBy = listViewModel.SortBy;
            SortDirection = listViewModel.SortDirection;
            PageIndex = listViewModel.PageIndex;
            PageSize = listViewModel.PageSize;
            SearchValue = listViewModel.SearchValue;
            SearchBy = listViewModel.SearchBy;

            return ListItems;
        }
    }

    public class RegionalSalesReportsListItem
    {
        public string Region { get; set; }
        public DateTime ReportRunDate { get; set; }
    }

    public class RegionalSalesReportsForm
    {
        public int Id { get; set; }
    }

    public class ModelWithNoId
    {
    }

    [NoIdentifier]
    public class ModelWithNoIdAttribute
    {
        public string Id { get; set; }
    }

    public class ModelWithId
    {
        public string Id { get; set; }
    }

    [Identifier("MyId")]
    public class ModelWithMyId : ModelWithId
    {
        public string MyId { get; set; }
    }
}