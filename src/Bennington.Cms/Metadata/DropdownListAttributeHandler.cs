using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public abstract class DropdownListAttributeHandler<T> : IMetadataAttributeHandler<T>
    {
        public abstract IEnumerable<SelectListItem> GetItems();

        protected DropdownListAttributeHandler()
        {
            PrependItemsWithSelector = true;
            SelectorText = "-- select --";
        }

        public bool PrependItemsWithSelector { get; set; }
        public string SelectorText { get; set; }

        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "Dropdown";

            metadata.AdditionalValues["SelectList"] = GetItemsForTheSelectList();
        }

        protected virtual List<SelectListItem> GetItemsForTheSelectList()
        {
            var items = GetItems().ToList();
            if (PrependItemsWithSelector)
                items.Insert(0, new SelectListItem {Text = SelectorText, Value = ""});
            return items;
        }
    }
}