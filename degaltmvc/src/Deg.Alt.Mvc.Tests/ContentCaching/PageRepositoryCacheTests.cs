using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.ContentCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.ContentCaching
{
    [TestClass]
    public class PageRepositoryCacheTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Returns_pages_from_repository_on_first_call()
        {
            var expectedPages = new Page[]{};

            SetupRepositoryToReturnThesePages(expectedPages);

            var repository = mocker.Resolve<PageRepositoryCache>();

            var pages = repository.GetPages();
            Assert.AreSame(expectedPages, pages);
        }

        [TestMethod]
        public void Return_the_same_results_on_the_second_call()
        {
            SetupPageRepositoryToReturnDifferentPagesOnEachCall();

            var repository = mocker.Resolve<PageRepositoryCache>();

            var results1 = repository.GetPages();
            var results2 = repository.GetPages();
            Assert.AreSame(results1, results2);
        }

        [TestMethod]
        public void Returns_different_results_when_the_cache_id_changes_between_calls()
        {
            SetupPageRepositoryToReturnDifferentPagesOnEachCall();
            var repository = mocker.Resolve<PageRepositoryCache>();

            SetupCacheId("one");
            var results1 = repository.GetPages();

            SetupCacheId("two");
            var results2 = repository.GetPages();

            Assert.AreNotSame(results1, results2);
        }

        [TestMethod]
        public void Returns_the_same_results_when_the_cache_id_does_not_change()
        {
            SetupPageRepositoryToReturnDifferentPagesOnEachCall();
            var repository = mocker.Resolve<PageRepositoryCache>();

            SetupCacheId("one");
            var results1 = repository.GetPages();
            var results2 = repository.GetPages();

            Assert.AreSame(results1, results2);
        }

        [TestMethod]
        public void Returns_the_page_repository_that_was_passed_in_the_constructor()
        {
            var expectedPageRepository = mocker.GetMock<IPageRepository>().Object;

            var repository = mocker.Resolve<PageRepositoryCache>();

            Assert.AreSame(expectedPageRepository, repository.PageRepository);
        }

        [TestMethod]
        public void Returns_the_content_cache_object_that_was_passed_in_the_constructor()
        {
            var contentCacheState = mocker.GetMock<IContentCacheState>().Object;

            var repository = mocker.Resolve<PageRepositoryCache>();

            Assert.AreSame(contentCacheState, repository.ContentCacheState);
        }

        private void SetupCacheId(string cacheId)
        {
            mocker.GetMock<IContentCacheState>()
                .Setup(x => x.GetCacheId())
                .Returns(cacheId);
        }

        private void SetupPageRepositoryToReturnDifferentPagesOnEachCall()
        {
            mocker.GetMock<IPageRepository>()
                .Setup(x => x.GetPages())
                .Returns(() => new Page[]{});
        }

        private void SetupRepositoryToReturnThesePages(Page[] expectedPages)
        {
            mocker.GetMock<IPageRepository>()
                .Setup(x => x.GetPages())
                .Returns(expectedPages);
        }
    }
}