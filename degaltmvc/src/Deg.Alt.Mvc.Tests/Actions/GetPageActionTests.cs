using System;
using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Actions
{
    [TestClass]
    public class GetPageActionTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void GetPage_PassedMatchingPageId_ReturnsPage()
        {
            // arrange
            var expectedPage = new Page{Id = "IdToMatch"};
            var pages = new[]{new Page(), expectedPage, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("IdToMatch");

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_PageIdDoesNotMatch_ReturnsNull()
        {
            // arrange
            var pages = new[]{new Page(), new Page{SectionId = "MatchingSectionId", Id = "MatchingPageId"}, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("Not Matching Page Id");

            // assert
            Assert.IsNull(page);
        }

        [TestMethod]
        public void GetPage_CaseInsensitiveMatchOnPageId_ReturnsPage()
        {
            // arrange
            var expectedPage = new Page{Id = "MatchingPageId"};

            var pages = new[]{new Page(), expectedPage, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("matchingpageid");

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_PassedMatchingSectionIdAndPageId_ReturnsPage()
        {
            // arrange
            var expectedPage = new Page{SectionId = "MatchingSectionId", Id = "MatchingPageId"};
            var pages = new[]{new Page(), expectedPage, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("MatchingSectionId", "MatchingPageId");

            // assert
            Assert.AreSame(expectedPage, page);
        }

        [TestMethod]
        public void GetPage_MatchingSectionIdButNotMatchingPageId_ReturnsNull()
        {
            // arrange
            var pages = new[]{new Page(), new Page{SectionId = "MatchingSectionId", Id = "MatchingPageId"}, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("MatchingSectionId", "Not Matching Page Id");

            // assert
            Assert.IsNull(page);
        }

        [TestMethod]
        public void GetPage_NotMatchingSectionIdButMatchingPageId_ReturnsNull()
        {
            // arrange
            var pages = new[]{new Page(), new Page{SectionId = "MatchingSectionId", Id = "MatchingPageId"}, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("Not Matching Section Id", "MatchingPageId");

            // assert
            Assert.IsNull(page);
        }

        [TestMethod]
        public void GetPage_CaseInsensitiveMatchOnSectionIdAndPageId_ReturnsPage()
        {
            // arrange
            var expectedPage = new Page{SectionId = "MatchingSectionId", Id = "MatchingPageId"};
            var pages = new[]{new Page(), expectedPage, new Page()};
            var repo = SetupPageRepositoryToReturnThesePages(pages);

            var action = new GetPageAction(new TestingServiceLocator(repo));

            // act
            var page = action.GetPage("matchingsectionid", "matchingpageid");

            // assert
            Assert.AreSame(expectedPage, page);
        }

        private IPageRepository SetupPageRepositoryToReturnThesePages(IEnumerable<Page> pages)
        {
            var pageRepositoryFake = new Mock<IPageRepository>();
            pageRepositoryFake.Setup(x => x.GetPages())
                .Returns(pages);
            return pageRepositoryFake.Object;
        }

        #region TestingServiceLocator

        public class TestingServiceLocator : IServiceLocator
        {
            private readonly IPageRepository pageRepository;

            public TestingServiceLocator(IPageRepository pageRepository)
            {
                this.pageRepository = pageRepository;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                return pageRepository as T;
            }

            public T Resolve<T>(string key) where T : class
            {
                throw new NotImplementedException();
            }

            public IList<T> ResolveServices<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IServiceRegistrar Batch()
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>() where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Interface instance) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Release(object instance)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public TService Inject<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }

            public void TearDown<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }

            public void Register(Type serviceType, Type implType)
            {
                throw new NotImplementedException();
            }

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Type implType) where Interface : class
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type)
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>(Type type) where T : class
            {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}