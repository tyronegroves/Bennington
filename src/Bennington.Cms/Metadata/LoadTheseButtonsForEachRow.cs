using System.Collections.Generic;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public abstract class LoadTheseButtonsForEachRow {
        private readonly IButtonRetriever buttonRetriever;

        protected LoadTheseButtonsForEachRow(IButtonRetriever buttonRetriever)
        {
            this.buttonRetriever = buttonRetriever;
        }

        public virtual IEnumerable<Button> GetButtons()
        {
            return new Button[] {};
        }

        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var model = args.ModelAccessor();
            metadata.AdditionalValues["IndividualRowButtons"] = buttonRetriever.GetButtonsForIndividualRow(args.ModelType, model);
        }
    }
}