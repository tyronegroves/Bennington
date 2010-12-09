using System.Web;
using AutoMoq;

namespace Deg.Alt.Mvc.Tests
{
    public static class MvcMockerHelpers
    {
        public static void SetupFakeHttpContext(this AutoMoqer mocker)
        {
            mocker.GetMock<HttpContextBase>()
                .Setup(x => x.Request)
                .Returns(mocker.GetMock<HttpRequestBase>().Object);

            mocker.GetMock<HttpContextBase>()
                .Setup(x => x.Response)
                .Returns(mocker.GetMock<HttpResponseBase>().Object);

            mocker.GetMock<HttpContextBase>()
                .Setup(x => x.Server)
                .Returns(mocker.GetMock<HttpServerUtilityBase>().Object);

            mocker.GetMock<HttpContextBase>()
                .Setup(x => x.Session)
                .Returns(mocker.GetMock<HttpSessionStateBase>().Object);
        }

        public static void SetupFakeHttpRequest(this AutoMoqer mocker, string url)
        {
            mocker.SetupFakeHttpRequest(url, "GET");
        }

        public static void SetupFakeHttpRequest(this AutoMoqer mocker, string url, string httpMethod)
        {
            mocker.GetMock<HttpRequestBase>()
                .Setup(x => x.AppRelativeCurrentExecutionFilePath)
                .Returns(url);

            mocker.GetMock<HttpRequestBase>()
                .Setup(x => x.HttpMethod)
                .Returns(httpMethod);
        }
    }
}