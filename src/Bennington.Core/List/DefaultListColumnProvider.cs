using System;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Core.Extensions;

namespace Bennington.Core.List
{
    public class DefaultListColumnProvider : IListColumnProvider
    {
        public ListColumns GetColumnsForType(Type type, ControllerContext controllerContext)
        {
            var columns = new ListColumns();
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, type);
            var sortingValues = GetSortingValues(controllerContext);

            foreach(var metadata in modelMetadata.Properties)
            {
                if(metadata.IsHidden()) continue;

                var column = new ListColumn
                                 {
                                     DisplayName = metadata.GetDisplayName(),
                                     Name = metadata.PropertyName,
                                     IsSearchable = metadata.IsSearchable(),
                                     Order = metadata.Order,
                                     SortDirection = sortingValues.SortDirection,
                                     Type = metadata.ModelType,
                                 };

                column.CellTemplate = FindCellTemplate(controllerContext, column.Name);
                column.HasCellTemplate = column.CellTemplate != null;
                column.IsSorted = column.Name.Equals(sortingValues.SortBy, StringComparison.InvariantCultureIgnoreCase);
                column.SortUrl = GetSortUrlForColumn(controllerContext, column, sortingValues);
                column.HeaderTemplate = FindHeaderTemplate(controllerContext, column.Name);
                column.HasHeaderTemplate = column.HeaderTemplate != null;

                columns.Add(column);
            }

            return columns;
        }

        private static SortingValues GetSortingValues(ControllerContext controllerContext)
        {
            var modelBindingContext = new ModelBindingContext
                                          {
                                              ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(SortingValues)),
                                              ValueProvider = ValueProviderFactories.Factories.GetValueProvider(controllerContext)
                                          };

            return (SortingValues)ModelBinders.Binders.GetBinder(typeof(SortingValues)).BindModel(controllerContext, modelBindingContext);
        }

        private static string GetSortUrlForColumn(ControllerContext controllerContext, ListColumn column, SortingValues sortingValues)
        {
            var routeValues = new RouteValueDictionary(new {sortBy = column.Name, searchBy = sortingValues.SearchBy, searchValue = sortingValues.SearchValue});

            if(column.IsSorted)
                routeValues.Add("sortdirection", column.SortDirection.Opposite());

            return new UrlHelper(controllerContext.RequestContext).Action(controllerContext.RouteData.GetRequiredString("action"), routeValues);
        }

        private static string FindHeaderTemplate(ControllerContext controllerContext, string name)
        {
            var headerTemplateLocation = string.Format("HeaderTemplates/{0}", name);
            return ViewEngines.Engines.PartialViewExists(controllerContext, headerTemplateLocation) ? headerTemplateLocation : null;
        }

        private static string FindCellTemplate(ControllerContext controllerContext, string name)
        {
            var cellTemplateLocation = string.Format("CellTemplates/{0}", name);
            return ViewEngines.Engines.PartialViewExists(controllerContext, cellTemplateLocation) ? cellTemplateLocation : null;
        }

        private class SortingValues
        {
            public string SortBy { get; set; }
            public SortDirection? SortDirection { get; set; }
            public string SearchBy { get; set; }
            public string SearchValue { get; set; }
        }
    }
}