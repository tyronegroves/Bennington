using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Bennington.Cms.Tests.Mocks;
using Bennington.Core.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class ListViewModelBinderTests
    {
        private ControllerContext controllerContext;
        private ListViewModel listViewModel;
        private ListColumns columns;
        private ListViewModelBinder binder;
        private ModelBindingContext bindingContext;
        private NameValueCollection form;
        private MockHttpContext httpContext;

        [TestInitialize]
        public void Setup()
        {
            form = new NameValueCollection();
            columns = new ListColumns();
            listViewModel = new ListViewModel {Columns = columns};
            httpContext = new MockHttpContext();
            ((MockHttpRequest)httpContext.Request).SetForm(form);

            controllerContext = new ControllerContext();
            controllerContext.HttpContext = httpContext;

            ValueProviderFactories.Factories
                .ReplaceWith<HttpFileCollectionValueProviderFactory>(ctx => ctx.HttpContext.Request.Form)
                .ReplaceWith<FormValueProviderFactory>(ctx => ctx.HttpContext.Request.Form)
                .ReplaceWith<QueryStringValueProviderFactory>(ctx => ctx.HttpContext.Request.QueryString);

            bindingContext = new ModelBindingContext
                                 {
                                     ModelMetadata = ModelMetadata.FromLambdaExpression(parameter => parameter, new ViewDataDictionary<ListViewModel>(listViewModel))
                                 };

            binder = new ListViewModelBinder();
        }

        [TestMethod]
        public void When_the_search_by_column_is_date_time_and_search_value_is_not_a_valid_date_time_an_error_is_returned()
        {
            columns.Add(new ListColumn {Name = "MyField", Type = typeof(DateTime)});
            form.Add("SearchBy", "MyField");
            form.Add("SearchValue", "12/2/2011");

            bindingContext.ValueProvider = ValueProviderFactories.Factories.GetValueProvider(controllerContext);
            binder.BindModel(controllerContext, bindingContext);

            Assert.AreEqual("'13/2/2011' is not a valid search value", bindingContext.ModelState["MyField"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void When_the_search_by_column_is_date_time_and_search_value_is_a_valid_date_time_no_error_is_returned()
        {
            //columns.Add(new ListColumn { Name = "MyField", Type = typeof(DateTime) });
            //listViewModel.SearchBy = "MyField";
            //listViewModel.SearchValue = new[]{"13/2/2011"};

            //var results = validator.Validate(listViewModel);

            //Assert.AreEqual(0, results.Count());
        }
    }
}