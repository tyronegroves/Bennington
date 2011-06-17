using System.Web.Mvc;
using Moq;

namespace Bennington.Cms.Tests
{
    public static class MetadataTestHelpers
    {
        public static ModelMetadata CreateModelMetadata(string propertyName = "x")
        {
            return new ModelMetadata(new Mock<ModelMetadataProvider>().Object, typeof (string), () => new object(), typeof (string), propertyName);
        }
    }
}