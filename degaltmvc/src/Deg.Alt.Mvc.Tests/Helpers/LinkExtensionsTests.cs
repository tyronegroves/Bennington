using System.Web.Mvc.Html;
using Deg.Alt.ContentProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Helpers
{
    [TestClass]
    public class LinkExtensionsTests
    {
        [TestMethod]
        public void InstinctActionLink_PassedUrl_ReturnsAppropriateHtml()
        {
            var helper = MvcHelper.GetHtmlHelper();

            var result = helper.InstinctActionLink("TEXT", "URL");

            Assert.AreEqual("<a href=\"URL\">TEXT</a>", result.ToString());
        }

        [TestMethod]
        public void InstinctActionLink_PassedPage_ReturnsAppropriateHtml()
        {
            var helper = MvcHelper.GetHtmlHelper();

            var result = helper.InstinctActionLink("TEXT", new Page{Url = "URL"});

            Assert.AreEqual("<a href=\"URL\">TEXT</a>", result.ToString());
        }

        [TestMethod]
        public void InstincActionLink_PassedUrlAndAttributeClass_ReturnsAppropriateHtml()
        {
            var helper = MvcHelper.GetHtmlHelper();

            var result = helper.InstinctActionLink("TEXT", "URL", new{@class = "testclass", style = "teststyle"});

            Assert.AreEqual("<a class=\"testclass\" href=\"URL\" style=\"teststyle\">TEXT</a>", result.ToString());
        }

        [TestMethod]
        public void InstinctActionLink_PassedPageAndAttributeClass_ReturnsAppropriateHtml()
        {
            var helper = MvcHelper.GetHtmlHelper();

            var result = helper.InstinctActionLink("TEXT", new Page{Url = "URL"}, new{@class = "testclass", style = "teststyle"});

            Assert.AreEqual("<a class=\"testclass\" href=\"URL\" style=\"teststyle\">TEXT</a>", result.ToString());
        }
    }
}