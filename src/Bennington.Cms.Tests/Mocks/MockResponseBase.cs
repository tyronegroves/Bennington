using System.Web;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockResponseBase : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}