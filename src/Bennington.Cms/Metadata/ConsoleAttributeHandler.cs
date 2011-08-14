using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class ConsoleAttribute : MetadataAttribute{}

    public abstract class ConsoleAttributeHandler<T> : IMetadataAttributeHandler<T>
    {
        public abstract IEnumerable<SelectListItem> GetItems();

        public string LeftLabel { get; set; }
        public string RightLabel { get; set; }

        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "Console";

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