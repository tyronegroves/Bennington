using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.Tests.Mocks;
using Bennington.Core.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class DefaultListColumnProviderTests
    {
        private Type listItemType;
        private ControllerContext controllerContext;
        private MockHttpContext httpContext;
        private RouteData routeData;
        private NameValueCollection queryString;
        private MockViewEngine viewEngine;

        [TestInitialize]
        public void Setup()
        {
            listItemType = typeof(TestListItem);
            queryString = new NameValueCollection();
            viewEngine = new MockViewEngine();

            ValueProviderFactories.Factories
                .ReplaceWith<HttpFileCollectionValueProviderFactory>(ctx => ctx.HttpContext.Request.Form)
                .ReplaceWith<FormValueProviderFactory>(ctx => ctx.HttpContext.Request.Form)
                .ReplaceWith<QueryStringValueProviderFactory>(ctx => ctx.HttpContext.Request.QueryString);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(viewEngine);

            httpContext = new MockHttpContext();
            ((MockHttpRequest)httpContext.Request).SetQueryString(queryString);
            routeData = new RouteData();
            routeData.Values.Add("action", "Index");

            controllerContext = new ControllerContext(httpContext, routeData, new TestController());
        }

        [TestCleanup]
        public void Cleanup()
        {
            using(RouteTable.Routes.GetWriteLock())
                RouteTable.Routes.Clear();
        }

        [TestMethod]
        public void List_of_columns_is_returned()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsInstanceOfType(columns, typeof(ListColumns));
        }

        [TestMethod]
        public void Property1_is_returned_with_the_name()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsNotNull(columns.Find("Property1"));
        }

        [TestMethod]
        public void Property1_is_returned_with_the_display_name()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("Property1 Display Name", listColumn.DisplayName);
        }

        [TestMethod]
        public void Property1_is_returned_with_the_is_searchable_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsFalse(listColumn.IsSearchable);
        }

        [TestMethod]
        public void Property2_is_returned_with_the_is_searchable_true()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsTrue(listColumn.IsSearchable);
        }

        [TestMethod]
        public void Property1_is_returned_with_default_order()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual(10000, listColumn.Order);
        }

        [TestMethod]
        public void Property2_is_returned_with_the_order()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual(1543, listColumn.Order);
        }

        [TestMethod]
        public void Property3_is_not_returned()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property3");

            Assert.IsNull(listColumn);
        }

        [TestMethod]
        public void Property1_is_returned_with_sortby_in_the_sort_url()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property1", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property1_is_returned_with_sortdirection_defaulted_to_descending_in_the_sort_url_when_the_column_is_sorted()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            queryString.Add("sortby", "property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);
            var listColumn = columns.Find("Property1");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property1&sortdirection=Descending", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property1_is_returned_with_sortdirection_set_to_ascending_in_the_sort_url_when_the_column_is_sorted()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            queryString.Add("sortby", "property1");
            queryString.Add("sortdirection", "descending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property1&sortdirection=Ascending", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property1_is_returned_with_sortdirection_set_to_descending_in_the_sort_url_when_the_column_is_sorted()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            queryString.Add("sortby", "property1");
            queryString.Add("sortdirection", "ascending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property1&sortdirection=Descending", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property2_is_returned_with_sortdirection_defaulted_to_descending_in_the_sort_url_when_the_column_is_sorted()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            queryString.Add("sortby", "property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);
            var listColumn = columns.Find("Property2");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property2&sortdirection=Descending", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property2_is_returned_with_sortdirection_set_to_ascending_in_the_sort_url_when_the_column_is_sorted()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            queryString.Add("sortby", "property2");
            queryString.Add("sortdirection", "descending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property2&sortdirection=Ascending", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property2_is_returned_with_sortdirection_set_to_descending_in_the_sort_url_when_the_column_is_sorted()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            queryString.Add("sortby", "property2");
            queryString.Add("sortdirection", "ascending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property2&sortdirection=Descending", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property2_is_returned_with_sortby_in_the_sort_url()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/a/{controller}/testing");

            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual("/myaction/a/mycontroller/testing?sortBy=Property2", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property1_is_returned_with_searchby_in_the_sort_url()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/t/{controller}/testing");

            queryString.Add("searchby", "property5");
            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("/myaction/t/mycontroller/testing?sortBy=Property1&searchBy=property5", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property2_is_returned_with_searchby_in_the_sort_url()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/b/{controller}/testing");

            queryString.Add("searchby", "property6");
            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual("/myaction/b/mycontroller/testing?sortBy=Property2&searchBy=property6", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property1_is_returned_with_searchvalue_in_the_sort_url()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/p/{controller}/testing");

            queryString.Add("searchvalue", "propert5");
            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("/myaction/p/mycontroller/testing?sortBy=Property1&searchValue=propert5", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property2_is_returned_with_searchvalue_in_the_sort_url()
        {
            var provider = new DefaultListColumnProvider();

            RouteTable.Routes.MapRoute("test", "{action}/c/{controller}/testing");

            queryString.Add("searchvalue", "property64");
            routeData.Values["action"] = "myaction";
            routeData.Values.Add("controller", "mycontroller");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual("/myaction/c/mycontroller/testing?sortBy=Property2&searchValue=property64", listColumn.SortUrl);
        }

        [TestMethod]
        public void Property1_is_returned_with_cell_template()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("CellTemplates/Property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual("CellTemplates/Property1", listColumn.CellTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_cell_template()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("CellTemplates/Property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual("CellTemplates/Property2", listColumn.CellTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_has_cell_template_true()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("CellTemplates/Property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsTrue(listColumn.HasCellTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_has_cell_template_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsFalse(listColumn.HasCellTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_has_cell_template_true()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("CellTemplates/Property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsTrue(listColumn.HasCellTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_has_cell_template_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsFalse(listColumn.HasCellTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_the_is_sorted_true()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsTrue(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property1_is_returned_with_the_is_sorted_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsFalse(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property2_is_returned_with_the_is_sorted_true()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsTrue(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property2_is_returned_with_the_is_sorted_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsFalse(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property1_is_returned_with_the_is_sorted_false_when_it_is_not_being_sorted()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "Property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsFalse(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property1_is_returned_with_the_is_sorted_true_when_it_is_being_sorted()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsTrue(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property2_is_returned_with_the_is_sorted_false_when_is_not_being_sorted()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "Pro");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsFalse(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property2_is_returned_with_the_is_sorted_true_when_it_is_being_sorted()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsTrue(listColumn.IsSorted);
        }

        [TestMethod]
        public void Property1_is_returned_with_sort_direction_set_to_null()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property13");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.IsNull(listColumn.SortDirection);
        }

        [TestMethod]
        public void Property1_is_returned_with_sort_direction_descending()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property1");
            queryString.Add("sortdirection", "descending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual(SortDirection.Descending, listColumn.SortDirection);
        }

        [TestMethod]
        public void Property1_is_returned_with_sort_direction_ascending()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property1");
            queryString.Add("sortdirection", "ascending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property1");

            Assert.AreEqual(SortDirection.Ascending, listColumn.SortDirection);
        }

        [TestMethod]
        public void Property2_is_returned_with_sort_direction_set_to_null()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property13");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.IsNull(listColumn.SortDirection);
        }

        [TestMethod]
        public void Property2_is_returned_with_sort_direction_descending()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property2");
            queryString.Add("sortdirection", "descending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual(SortDirection.Descending, listColumn.SortDirection);
        }

        [TestMethod]
        public void Property2_is_returned_with_sort_direction_ascending()
        {
            var provider = new DefaultListColumnProvider();

            queryString.Add("sortby", "property2");
            queryString.Add("sortdirection", "ascending");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            var listColumn = columns.Find("Property2");

            Assert.AreEqual(SortDirection.Ascending, listColumn.SortDirection);
        }

        [TestMethod]
        public void List_columns_are_ordered_when_returned()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.AreEqual("Property4", columns.ElementAt(0).Name);
            Assert.AreEqual("Property2", columns.ElementAt(1).Name);
            Assert.AreEqual("Property1", columns.ElementAt(2).Name);
        }

        [TestMethod]
        public void Property1_is_returned_with_header_template()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("HeaderTemplates/Property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.AreEqual("HeaderTemplates/Property1", columns.Find("Property1").HeaderTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_header_template()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("HeaderTemplates/Property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.AreEqual("HeaderTemplates/Property2", columns.Find("Property2").HeaderTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_no_header_template()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsNull(columns.Find("Property1").HeaderTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_no_header_template()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsNull(columns.Find("Property2").HeaderTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_has_header_template_set_to_true()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("HeaderTemplates/Property1");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsTrue(columns.Find("Property1").HasHeaderTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_has_header_template_set_to_true()
        {
            var provider = new DefaultListColumnProvider();

            viewEngine.MakeViewExists("HeaderTemplates/Property2");

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsTrue(columns.Find("Property2").HasHeaderTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_has_header_template_set_to_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsFalse(columns.Find("Property1").HasHeaderTemplate);
        }

        [TestMethod]
        public void Property2_is_returned_with_has_header_template_set_to_false()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.IsFalse(columns.Find("Property2").HasHeaderTemplate);
        }

        [TestMethod]
        public void Property1_is_returned_with_type()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.AreEqual(typeof(string), columns.Find("Property1").Type);
        }

        [TestMethod]
        public void Property2_is_returned_with_type()
        {
            var provider = new DefaultListColumnProvider();

            var columns = provider.GetColumnsForType(listItemType, controllerContext);

            Assert.AreEqual(typeof(int), columns.Find("Property2").Type);
        }
    }

    public class TestController : Controller
    {
    }

    public class TestListItem
    {
        [Display(Name = "Property1 Display Name")]
        public string Property1 { get; set; }

        [Display(Order = 1543), Searchable]
        public int Property2 { get; set; }

        [Hidden]
        public long Property3 { get; set; }

        [Display(Order = 23)]
        public uint Property4 { get; set; }
    }
}