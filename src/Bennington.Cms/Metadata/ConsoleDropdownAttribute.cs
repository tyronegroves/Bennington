using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class TypeAheadListAttribute : MetadataAttribute { }

    public abstract class TypeAheadListAttributeHandler<T> : IMetadataAttributeHandler<T>
    {
        public abstract IEnumerable<SelectListItem> GetItems();

        public string LeftLabel { get; set; }
        public string RightLabel { get; set; }

        public virtual void AlterMetadata(System.Web.Mvc.ModelMetadata metadata, MvcTurbine.Web.Metadata.CreateMetadataArguments args)
        {
            metadata.TemplateHint = "TypeAheadList";

            metadata.AdditionalValues["SelectList"] = GetItemsForTheSelectList();

            metadata.AdditionalValues["OverrideLeftLabel"] = LeftLabel;
            metadata.AdditionalValues["OverrideRightLabel"] = RightLabel;
        }

        protected virtual List<SelectListItem> GetItemsForTheSelectList()
        {
            var items = GetItems().ToList();
            return items;
        }
    }
}