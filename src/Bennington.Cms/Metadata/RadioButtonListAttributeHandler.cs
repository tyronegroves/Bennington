using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public abstract class RadioButtonListAttributeHandler<T> : IMetadataAttributeHandler<T>
    {
        public abstract IEnumerable<SelectListItem> GetItems();

        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "RadioButtonList";

            metadata.AdditionalValues["SelectList"] = GetItemsForTheSelectList();
        }

        private List<SelectListItem> GetItemsForTheSelectList()
        {
            return GetItems().ToList();
        }
    }
}