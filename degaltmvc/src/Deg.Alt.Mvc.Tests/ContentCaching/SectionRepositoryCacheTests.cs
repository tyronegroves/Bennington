using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.ContentCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.ContentCaching
{
    [TestClass]
    public class SectionRepositoryCacheTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Returns_sections_from_repository_on_first_call()
        {
            var expectedSections = new Section[]{};

            SetupRepositoryToReturnTheseSections(expectedSections);

            var repository = mocker.Resolve<SectionRepositoryCache>();

            var sections = repository.GetSections();
            Assert.AreSame(expectedSections, sections);
        }

        [TestMethod]
        public void Return_the_same_results_on_the_second_call()
        {
            SetupSectionRepositoryToReturnDifferentSectionsOnEachCall();

            var repository = mocker.Resolve<SectionRepositoryCache>();

            var results1 = repository.GetSections();
            var results2 = repository.GetSections();
            Assert.AreSame(results1, results2);
        }

        [TestMethod]
        public void Returns_different_results_when_the_cache_id_changes_between_calls()
        {
            SetupSectionRepositoryToReturnDifferentSectionsOnEachCall();
            var repository = mocker.Resolve<SectionRepositoryCache>();

            SetupCacheId("one");
            var results1 = repository.GetSections();

            SetupCacheId("two");
            var results2 = repository.GetSections();

            Assert.AreNotSame(results1, results2);
        }

        [TestMethod]
        public void Returns_the_same_results_when_the_cache_id_does_not_change()
        {
            SetupSectionRepositoryToReturnDifferentSectionsOnEachCall();
            var repository = mocker.Resolve<SectionRepositoryCache>();

            SetupCacheId("one");
            var results1 = repository.GetSections();
            var results2 = repository.GetSections();

            Assert.AreSame(results1, results2);
        }

        [TestMethod]
        public void Returns_the_section_repository_that_was_passed_in_the_constructor()
        {
            var expectedSectionRepository = mocker.GetMock<ISectionRepository>().Object;

            var repository = mocker.Resolve<SectionRepositoryCache>();

            Assert.AreSame(expectedSectionRepository, repository.SectionRepository);
        }

        [TestMethod]
        public void Returns_the_content_cache_object_that_was_passed_in_the_constructor()
        {
            var contentCacheState = mocker.GetMock<IContentCacheState>().Object;

            var repository = mocker.Resolve<SectionRepositoryCache>();

            Assert.AreSame(contentCacheState, repository.ContentCacheState);
        }

        private void SetupCacheId(string cacheId)
        {
            mocker.GetMock<IContentCacheState>()
                .Setup(x => x.GetCacheId())
                .Returns(cacheId);
        }

        private void SetupSectionRepositoryToReturnDifferentSectionsOnEachCall()
        {
            mocker.GetMock<ISectionRepository>()
                .Setup(x => x.GetSections())
                .Returns(() => new Section[]{});
        }

        private void SetupRepositoryToReturnTheseSections(Section[] expectedSections)
        {
            mocker.GetMock<ISectionRepository>()
                .Setup(x => x.GetSections())
                .Returns(expectedSections);
        }
    }
}