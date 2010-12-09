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
    public class GetControllerForPageIdActionTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void GetController_ControllerForPageIdIsExpected_ReturnsExpected()
        {
            // arrange
            var repo = SetupRepositoryToReturnPages(new[]{
                                                             new Page{Id = "One", Controller = "Unexpected"},
                                                             new Page{Id = "Two", Controller = "Expected"},
                                                             new Page{Id = "Three", Controller = "Unexpected"},
                                                         });

            var action = new GetControllerForPageIdAction(new TestingServiceLocator(repo));

            // act
            var controller = action.GetController("Two");

            // assert
            Assert.AreEqual("Expected", controller);
        }

        [TestMethod]
        public void GetController_PageIdMatchesWithCaseInsensitiveMatch_ReturnsExpected()
        {
            // arrange

            var repo = SetupRepositoryToReturnPages(new[]{
                                                             new Page{Id = "One", Controller = "Unexpected"},
                                                             new Page{Id = "TWO", Controller = "Expected"},
                                                             new Page{Id = "Three", Controller = "Unexpected"},
                                                         });

            var action = new GetControllerForPageIdAction(new TestingServiceLocator(repo));

            // act
            var controller = action.GetController("Two");

            // assert
            Assert.AreEqual("Expected", controller);
        }

        [TestMethod]
        public void GetController_PageIdDoesNotMatchController_ReturnsNull()
        {
            // arrange
            var pages = new[]{
                                 new Page{Id = "One", Controller = "Unexpected"},
                                 new Page{Id = "Two", Controller = "Expected"},
                                 new Page{Id = "Three", Controller = "Unexpected"},
                             };
            var repo = SetupRepositoryToReturnPages(pages);

            var action = new GetControllerForPageIdAction(new TestingServiceLocator(repo));

            // act
            var controller = action.GetController("No Match");

            // assert
            Assert.IsNull(controller);
        }

        private IPageRepository SetupRepositoryToReturnPages(Page[] pages)
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