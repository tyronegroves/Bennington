using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Bennington.Core.Extensions;

namespace Bennington.Core.List
{
    public class DefaultListViewModelProvider : IListViewModelProvider
    {
        private readonly IListColumnProvider listColumnProvider;

        public DefaultListViewModelProvider() : this(new DefaultListColumnProvider())
        {
        }

        protected DefaultListViewModelProvider(IListColumnProvider listColumnProvider)
        {
            this.listColumnProvider = listColumnProvider;
        }

        public ListViewModel GetListViewModelForType(Type type, ControllerContext controllerContext, ListViewModelOptions options)
        {
            var urlHelper = new UrlHelper(controllerContext.RequestContext);
            var listViewModel = new ListViewModel {Columns = listColumnProvider.GetColumnsForType(type, controllerContext)};

            UpdateModel(listViewModel, controllerContext, options.ListViewModelIncludeProperties, options.ListViewModelExcludeProperties);

            if(options.RenderOptions != null)
                listViewModel.RenderOptions = options.RenderOptions;

            listViewModel.SearchUrl = urlHelper.Action(options.SearchAction, new {listViewModel.SortBy, listViewModel.SortDirection});
            listViewModel.TitleViewName = ViewEngines.Engines.FindPartialViewOrDefault(controllerContext, options.TitleViewName, options.DefaultTitleViewName);
            listViewModel.ButtonsViewName = ViewEngines.Engines.FindPartialViewOrDefault(controllerContext, options.ButtonsViewName, options.DefaultButtonsViewName);
            listViewModel.PagerViewName = ViewEngines.Engines.FindPartialViewOrDefault(controllerContext, options.PagerViewName, options.DefaultPagerViewName);
            listViewModel.RowsViewName = ViewEngines.Engines.FindPartialViewOrDefault(controllerContext, options.RowsViewName, options.DefaultRowsViewName);
            listViewModel.SearchViewName = ViewEngines.Engines.FindPartialViewOrDefault(controllerContext, options.SearchFormViewName, options.DefaultSearchFormViewName);
            listViewModel.Title = options.Title ?? GetDefaultTitle(controllerContext.Controller);
            listViewModel.Subtitle = options.Subtitle;

            return listViewModel;
        }

        private static string GetDefaultTitle(ControllerBase controller)
        {
            var title = controller.GetType().Name.Replace("Controller", "");
            return Regex.Replace(title, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        private static void UpdateModel(ListViewModel listViewModel, ControllerContext controllerContext, string[] includeProperties, string[] excludeProperties)
        {
            var modelBindingContext = new ModelBindingContext
                                          {
                                              ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => listViewModel, listViewModel.GetType()),
                                              ValueProvider = ValueProviderFactories.Factories.GetValueProvider(controllerContext),
                                              ModelState = listViewModel.ModelState,
                                              PropertyFilter = propertyName => IsPropertyAllowed(propertyName, includeProperties, excludeProperties),
                                          };

            ModelBinders.Binders.GetBinder(typeof(ListViewModel)).BindModel(controllerContext, modelBindingContext);
            
        }

        private static bool IsPropertyAllowed(string propertyName, string[] includeProperties, string[] excludeProperties)
        {
            var flag = ((includeProperties == null) || (includeProperties.Length == 0)) || includeProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
            var flag2 = (excludeProperties != null) && excludeProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
            return (flag && !flag2);
        }
    }
}