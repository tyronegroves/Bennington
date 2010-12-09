using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.ContentProvider.RelatedItemReaders;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Actions
{
    [TestClass]
    public class GetContentItemForPageLocationActionTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void GetContentItem_IdOnContentItemMatchesStep_ReturnsContentItem()
        {
            var expectedContentItem = new ContentItem{Id = "ExpectedId"};

            var pageLocation = new PageLocation{Step = "ExpectedId"};
            SetupRelatedItemsToReturnForPageLocation(pageLocation, new[]{new object(), expectedContentItem, new object()});

            var contentItem = GetContentItem(pageLocation);
            Assert.AreSame(expectedContentItem, contentItem);
        }

        [TestMethod]
        public void GetContentItem_IdOnContentItemIsACaseInsensitiveMatch_ReturnsContentItem()
        {
            var expectedContentItem = new ContentItem { Id = "expectedid" };

            var pageLocation = new PageLocation { Step = "ExpectedId" };
            SetupRelatedItemsToReturnForPageLocation(pageLocation, new[] { new object(), expectedContentItem, new object() });

            var contentItem = GetContentItem(pageLocation);
            Assert.AreSame(expectedContentItem, contentItem);
        }

        [TestMethod]
        public void GetContentItem_IdDoesNotMatch_ReturnsNull()
        {
            var expectedContentItem = new ContentItem{Id = "ExpectedId"};

            var pageLocation = new PageLocation{Step = "Id That Does Not Match"};
            SetupRelatedItemsToReturnForPageLocation(pageLocation, new[]{expectedContentItem});

            var contentItem = GetContentItem(pageLocation);
            Assert.IsNull(contentItem);
        }

        [TestMethod]
        public void GetContentItem_RelatedItemsOnPageIsNull_ReturnsNull()
        {
            var pageLocation = new PageLocation();
            SetupRelatedItemsToReturnForPageLocation(pageLocation, null);

            var contentItem = GetContentItem(pageLocation);
            Assert.IsNull(contentItem);
        }

        [TestMethod]
        public void GetContentItem_PageDoesNotExistForPageLocation_ReturnsNull()
        {
            var pageLocation = new PageLocation();

            mocker.GetMock<IGetPageForPageLocationAction>()
                .Setup(x => x.GetPage(pageLocation))
                .Returns((PageLocation pg) => null);

            var contentItem = GetContentItem(pageLocation);
            Assert.IsNull(contentItem);
        }

        private ContentItem GetContentItem(PageLocation pageLocation)
        {
            var action = mocker.Resolve<GetContentItemForPageLocationAction>();

            return action.GetContentItem(pageLocation);
        }

        private void SetupRelatedItemsToReturnForPageLocation(PageLocation pageLocation, IEnumerable<object> relatedItems)
        {
            mocker.GetMock<IGetPageForPageLocationAction>()
                .Setup(x => x.GetPage(pageLocation))
                .Returns(new Page{RelatedItems = relatedItems});
        }
    }
}