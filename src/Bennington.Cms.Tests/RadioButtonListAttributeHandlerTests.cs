using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Metadata;
using MvcTurbine.Web.Metadata;
using NUnit.Framework;
using Should;

namespace Bennington.Cms.Tests
{
    [TestFixture]
    public class RadioButtonListAttributeHandlerTests
    {
        [Test]
        public void The_template_hint_should_be_set_to_dropdown_list()
        {
            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestRadioButtonListAttributeHandler();
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            metadata.TemplateHint.ShouldEqual("RadioButtonList");
        }

        [Test]
        public void The_results_of_the_GetItems_method_should_be_set_to_SelectList_in_AdditionalItems()
        {
            var first = new SelectListItem();
            var second = new SelectListItem();
            var expectedItems = new[] {first, second};

            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestRadioButtonListAttributeHandler();
            handler.TheseItemsWillBeReturned(expectedItems);
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            var selectList = metadata.AdditionalValues["SelectList"].CastAs<IEnumerable<SelectListItem>>();
            selectList.Contains(first);
            selectList.Contains(second);
        }

        private class TestingAttribute : MetadataAttribute
        {
        }

        private class TestRadioButtonListAttributeHandler : RadioButtonListAttributeHandler<TestingAttribute>
        {
            private IEnumerable<SelectListItem> selectListItem = new List<SelectListItem>();

            public void TheseItemsWillBeReturned(IEnumerable<SelectListItem> selectListItem)
            {
                this.selectListItem = selectListItem;
            }

            public override IEnumerable<SelectListItem> GetItems()
            {
                return selectListItem;
            }
        }
    }
}