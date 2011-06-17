using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bennington.Cms.Metadata;
using MvcTurbine.Web.Metadata;
using NUnit.Framework;
using Should;

namespace Bennington.Cms.Tests
{
    [TestFixture]
    public class DropdownListAttributeHandlerTests
    {
        [Test]
        public void The_template_hint_should_be_set_to_dropdown()
        {
            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestDropdownListAttributeHandler();
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            metadata.TemplateHint.ShouldEqual("Dropdown");
        }

        [Test]
        public void The_results_of_the_GetItems_method_should_be_set_to_SelectList_in_AdditionalItems()
        {
            var first = new SelectListItem();
            var second = new SelectListItem();
            var expectedItems = new[] { first, second };

            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestDropdownListAttributeHandler();
            handler.TheseItemsWillBeReturned(expectedItems);
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            var selectList = metadata.AdditionalValues["SelectList"].CastAs<IEnumerable<SelectListItem>>();
            selectList.Contains(first);
            selectList.Contains(second);
        }

        [Test]
        public void The_first_result_should_be_an_empty_item_when_PrependItemsWithSelector_is_true()
        {
            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestDropdownListAttributeHandler();
            handler.TheseItemsWillBeReturned(new SelectListItem[] { });
            handler.PrependItemsWithSelector = true;
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            var selectList = metadata.AdditionalValues["SelectList"].CastAs<IEnumerable<SelectListItem>>();
            selectList.First().Text.ShouldEqual("-- select --");
            selectList.First().Value.ShouldEqual("");
        }

        [Test]
        public void The_first_result_should_not_be_an_empty_item_when_PreprendItemsWithSelector_is_false()
        {
            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestDropdownListAttributeHandler();
            handler.TheseItemsWillBeReturned(new SelectListItem[] { });
            handler.PrependItemsWithSelector = false;
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            var selectList = metadata.AdditionalValues["SelectList"].CastAs<IEnumerable<SelectListItem>>();
            selectList.Count().ShouldEqual(0);
        }

        [Test]
        public void The_PrependItemsWithSelector_is_true_by_default()
        {
            var handler = new TestDropdownListAttributeHandler();
            handler.PrependItemsWithSelector.ShouldBeTrue();
        }

        [Test]
        public void The_Selector_text_defaults_to_dashdash_select_dashdash()
        {
            var handler = new TestDropdownListAttributeHandler();
            handler.SelectorText.ShouldEqual("-- select --");
        }

        [Test]
        public void Uses_the_selector_text()
        {
            var metadata = MetadataTestHelpers.CreateModelMetadata();

            var handler = new TestDropdownListAttributeHandler();
            handler.SelectorText = "This Is The Expected Result";
            handler.AlterMetadata(metadata, new CreateMetadataArguments());

            var results = metadata.AdditionalValues["SelectList"].CastAs<IEnumerable<SelectListItem>>();

            results.First().Value.ShouldEqual("");
            results.First().Text.ShouldEqual("This Is The Expected Result");
        }

        private class TestingAttribute : MetadataAttribute
        {
        }

        private class TestDropdownListAttributeHandler : DropdownListAttributeHandler<TestingAttribute>
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
