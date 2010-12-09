using AutoMoq;
using Deg.Alt.Mvc.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Helpers
{
    [TestClass]
    public class TagReplacerTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Adding_a_replace_tag_causes_the_tag_to_be_replaced()
        {
            var replacer = mocker.Resolve<TagReplacer>();

            replacer.AddReplaceTag("FirstName", "John");

            var result = replacer.ProcessReplacementTags("Hello [:FirstName:]!");

            Assert.AreEqual("Hello John!", result);
        }

        [TestMethod]
        public void Not_adding_a_replace_tag_will_result_in_no_change()
        {
            var replacer = mocker.Resolve<TagReplacer>();

            var result = replacer.ProcessReplacementTags("Hello [:FirstName:]!");

            Assert.AreEqual("Hello [:FirstName:]!", result);
        }

        [TestMethod]
        public void Adding_a_replace_tag_twice_causes_the_last_value_to_be_used()
        {
            var replacer = mocker.Resolve<TagReplacer>();

            replacer.AddReplaceTag("FirstName", "John");
            replacer.AddReplaceTag("FirstName", "Jane");

            var result = replacer.ProcessReplacementTags("Hello [:FirstName:]!");

            Assert.AreEqual("Hello Jane!", result);
        }
    }
}