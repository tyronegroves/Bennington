using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class SetSectionHeaderOnListPageToAttribute : MetadataAttribute
    {
        public SetSectionHeaderOnListPageToAttribute(string header)
        {
            Header = header;
        }

        public string Header { get; set; }
    }

    public class SetSectionHeaderOnListPageToAttributeHandler :
        IMetadataAttributeHandler<SetSectionHeaderOnListPageToAttribute>
    {
        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var attribute = args.Attributes
                .OfType<SetSectionHeaderOnListPageToAttribute>()
                .First();

            metadata.AdditionalValues["SectionHeader"] = attribute.Header;
        }
    }
}