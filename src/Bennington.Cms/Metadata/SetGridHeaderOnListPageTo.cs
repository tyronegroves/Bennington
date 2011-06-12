using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class SetGridHeaderOnListPageToAttribute : MetadataAttribute
    {
        public SetGridHeaderOnListPageToAttribute(string header)
        {
            Header = header;
        }

        public string Header { get; set; }
    }

    public class SetGridHeaderOnListPageToAttributeHandler :
        IMetadataAttributeHandler<SetGridHeaderOnListPageToAttribute>
    {
        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var attribute = args.Attributes
                .OfType<SetGridHeaderOnListPageToAttribute>()
                .First();

            metadata.AdditionalValues["GridHeader"] = attribute.Header;
        }
    }
}