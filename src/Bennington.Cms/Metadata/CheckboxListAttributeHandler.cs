using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public abstract class CheckboxListAttributeHandler<T> : IMetadataAttributeHandler<T>
    {
        public abstract IEnumerable<SelectListItem> GetItems();

        public string SelectorText { get; set; }

        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "CheckboxList";

            metadata.AdditionalValues["SelectList"] = GetItemsForTheSelectList();
        }

        protected virtual List<SelectListItem> GetItemsForTheSelectList()
        {
            return GetItems().ToList();
        }
    }
}