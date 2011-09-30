using System.Web;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockHttpContext : HttpContextBase
    {
        private readonly HttpRequestBase request;
        private readonly HttpResponseBase response;

        public MockHttpContext()
            : this(new MockHttpRequest(), new MockResponseBase())
        {
        }

        public MockHttpContext(HttpRequestBase request, HttpResponseBase response)
        {
            this.request = request;
            this.response = response;
        }

        public override HttpRequestBase Request
        {
            get { return request; }
        }

        public override HttpResponseBase Response
        {
            get{return response;}
        }
    }
}