using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Actions
{
    [TestClass]
    public class GetPageForPageLocationActionTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void GetPage_PageWithMatchingPageIdExists_ReturnsPage()
        {
            // arrange
            var expectedPage = new Page{Id = "ExpectedPageId"};

            SetupPageRepositoryToReturnThesePages(new[]{new Page(), expectedPage, new Page(),});

            var action = mocker.Resolve<GetPageForPageLocationAction>();

            // act
            var page = action.GetPage(new PageLocation{PageId = "ExpectedPageId"});

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_PageWithCaseInsensitiveMatchOnPageIdExists_ReturnsPage()
        {
            // arrange
            var expectedPage = new Page{Id = "expectedpageid"};

            SetupPageRepositoryToReturnThesePages(new[]{new Page(), expectedPage, new Page(),});

            var action = mocker.Resolve<GetPageForPageLocationAction>();

            // act
            var page = action.GetPage(new PageLocation{PageId = "ExpectedPageId"});

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_NoMatchingPageIdButMatchingSectionId_ReturnsDefaultPageForSection()
        {
            // arrange
            var expectedPage = new Page{SectionId = "ExpectedSectionId", Key = 24};

            SetupPageRepositoryToReturnThesePages(new[]{new Page(), expectedPage, new Page(),});
            SetupSectionRepositoryToReturnTheseSections(new[]{new Section(), new Section{Id = "ExpectedSectionId", DefaultPageKey = 24}, new Section(),});

            var action = mocker.Resolve<GetPageForPageLocationAction>();

            // act
            var page = action.GetPage(new PageLocation{PageId = "x", SectionId = "ExpectedSectionId"});

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_NoMatchingPageIdButCaseInsensitiveMatchingSectionId_ReturnsDefaultPageForSection()
        {
            // arrange
            var expectedPage = new Page{SectionId = "expectedsectionid", Key = 24};

            SetupPageRepositoryToReturnThesePages(new[]{new Page(), expectedPage, new Page(),});
            SetupSectionRepositoryToReturnTheseSections(new[]{new Section(), new Section{Id = "expectedsectionid", DefaultPageKey = 24}, new Section(),});

            var action = mocker.Resolve<GetPageForPageLocationAction>();

            // act
            var page = action.GetPage(new PageLocation{PageId = "x", SectionId = "ExpectedSectionId"});

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_NoMatchOnPageIdOrSection_ReturnsNull()
        {
            // arrange
            SetupPageRepositoryToReturnThesePages(new Page[]{});
            SetupSectionRepositoryToReturnTheseSections(new Section[]{});

            var action = mocker.Resolve<GetPageForPageLocationAction>();

            // act
            var page = action.GetPage(new PageLocation());

            // assert
            Assert.IsNull(page);
        }

        private void SetupSectionRepositoryToReturnTheseSections(IEnumerable<Section> sections)
        {
            mocker.GetMock<ISectionRepository>()
                .Setup(x => x.GetSections())
                .Returns(sections);
        }

        private void SetupPageRepositoryToReturnThesePages(IEnumerable<Page> pages)
        {
            mocker.GetMock<IPageRepository>()
                .Setup(x => x.GetPages())
                .Returns(pages);
        }
    }
}