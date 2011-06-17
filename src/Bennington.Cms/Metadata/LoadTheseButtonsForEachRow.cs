using System.Collections.Generic;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public abstract class LoadTheseButtonsForEachRow<T> where T : class
    {
        public virtual IEnumerable<Button> GetButtons(T model)
        {
            return new Button[] {};
        }

        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var model = args.ModelAccessor();
            if (model == null) return;
            metadata.AdditionalValues["IndividualRowButtons"] = GetButtons(model as T);
        }
    }
}