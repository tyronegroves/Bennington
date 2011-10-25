using System.ComponentModel;
using System.Web.Mvc;
using Bennington.Core.Extensions;

namespace Bennington.Core.List
{
    public class ListViewModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if(propertyDescriptor.Name == "SearchValue")
            {
                var model = bindingContext.Model as ListViewModel;
                var searchBy = bindingContext.ValueProvider.GetValue<string>("SearchBy");
                var searchValueResult = bindingContext.ValueProvider.GetValue("SearchValue");

                if(model == null || searchBy == null || searchValueResult == null) return;

                var searchByColumn = model.Columns.Find(searchBy);

                try
                {
                    model.SearchValue = searchValueResult.ConvertTo(searchByColumn.Type);
                }
                catch
                {
                    bindingContext.ModelState.AddModelError(propertyDescriptor.Name, string.Format("'{0}' is not a valid search value.", searchValueResult.AttemptedValue));
                }

                return;
            }

            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}