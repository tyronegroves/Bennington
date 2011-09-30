using System.Collections.Specialized;
using System.Web;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockHttpRequest : HttpRequestBase
    {
        private string applicationPath;

        public void SetApplicationPath(string appPath)
        {
            applicationPath = appPath;
        }

        public override string ApplicationPath
        {
            get { return applicationPath; }
        }

        public override NameValueCollection ServerVariables
        {
            get { return new NameValueCollection(); }
        }
    }
}