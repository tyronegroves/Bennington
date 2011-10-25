using System.Collections.Specialized;
using System.Web;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockHttpRequest : HttpRequestBase
    {
        private string applicationPath;
        private string contentType = string.Empty;
        private NameValueCollection queryString = new NameValueCollection();
        private NameValueCollection form = new NameValueCollection();

        public void SetApplicationPath(string appPath)
        {
            applicationPath = appPath;
        }

        public override string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        public override string ApplicationPath
        {
            get { return applicationPath; }
        }

        public override NameValueCollection ServerVariables
        {
            get { return new NameValueCollection(); }
        }

        public void SetQueryString(NameValueCollection newQueryString)
        {
            queryString = newQueryString;
        }

        public override NameValueCollection QueryString
        {
            get { return queryString; }
        }

        public override NameValueCollection Form
        {
            get { return form; }
        }

        public void SetForm(NameValueCollection newForm)
        {
            form = newForm;
        }
    }
}