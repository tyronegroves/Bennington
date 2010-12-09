using System;
using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Blades;
using Deg.Alt.Mvc.ContentCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTurbine;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Blades
{
    [TestClass]
    public class ContentCachingBladeTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void When_a_content_cache_state_and_a_page_repository_is_registered_then_register_a_page_repository_cache_object()
        {
            var testingServiceLocator = new TestingServiceLocator(new Mock<IPageRepository>().Object,
                                                                  new Mock<ISectionRepository>().Object,
                                                                  new Mock<IContentCacheState>().Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            Assert.IsNotNull(testingServiceLocator.PageRepositoryRegisteredByMethod);
            Assert.IsInstanceOfType(testingServiceLocator.PageRepositoryRegisteredByMethod, typeof (PageRepositoryCache));
        }

        [TestMethod]
        public void When_a_content_cache_state_and_a_page_repository_is_registered_then_register_the_cache_with_the_page_repository()
        {
            var expectedPageRepository = new Mock<IPageRepository>().Object;
            var testingServiceLocator = new TestingServiceLocator(expectedPageRepository, new Mock<ISectionRepository>().Object, new Mock<IContentCacheState>().Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            var result = testingServiceLocator.PageRepositoryRegisteredByMethod as PageRepositoryCache;
            Assert.AreSame(expectedPageRepository, result.PageRepository);
        }

        [TestMethod]
        public void When_a_content_cache_state_and_a_page_repository_is_registered_then_register_the_cache_with_the_content_cache_state()
        {
            var expectecContentCacheState = new Mock<IContentCacheState>();
            var testingServiceLocator = new TestingServiceLocator(new Mock<IPageRepository>().Object, new Mock<ISectionRepository>().Object, expectecContentCacheState.Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            var result = testingServiceLocator.PageRepositoryRegisteredByMethod as PageRepositoryCache;
            Assert.AreSame(expectecContentCacheState.Object, result.ContentCacheState);
        }

        [TestMethod]
        public void When_a_content_cache_state_and_a_stionec_repository_is_registered_then_register_a_section_repository_cache_object()
        {
            var testingServiceLocator = new TestingServiceLocator(new Mock<IPageRepository>().Object,
                                                                  new Mock<ISectionRepository>().Object,
                                                                  new Mock<IContentCacheState>().Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            Assert.IsNotNull(testingServiceLocator.SectionRepositoryRegisteredByMethod);
            Assert.IsInstanceOfType(testingServiceLocator.SectionRepositoryRegisteredByMethod, typeof (SectionRepositoryCache));
        }

        [TestMethod]
        public void When_a_content_cache_state_and_a_section_repository_is_registered_then_register_the_cache_with_the_section_repository()
        {
            var expectedSectionRepository = new Mock<ISectionRepository>().Object;
            var testingServiceLocator = new TestingServiceLocator(new Mock<IPageRepository>().Object, expectedSectionRepository, new Mock<IContentCacheState>().Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            var result = testingServiceLocator.SectionRepositoryRegisteredByMethod as SectionRepositoryCache;
            Assert.AreSame(expectedSectionRepository, result.SectionRepository);
        }

        [TestMethod]
        public void When_a_content_cache_state_and_a_section_repository_is_registered_then_register_the_cache_with_the_content_cache_state()
        {
            var expectecContentCacheState = new Mock<IContentCacheState>();
            var testingServiceLocator = new TestingServiceLocator(new Mock<IPageRepository>().Object, new Mock<ISectionRepository>().Object, expectecContentCacheState.Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            var result = testingServiceLocator.SectionRepositoryRegisteredByMethod as SectionRepositoryCache;
            Assert.AreSame(expectecContentCacheState.Object, result.ContentCacheState);
        }

        [TestMethod]
        public void When_no_content_cache_state_is_registered_then_do_nothing()
        {
            var testingServiceLocator = new TestingServiceLocator(new Mock<IPageRepository>().Object, new Mock<ISectionRepository>().Object, null);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            Assert.IsNull(testingServiceLocator.PageRepositoryRegisteredByMethod);
        }

        [TestMethod]
        public void When_no_page_repository_is_registered_then_do_nothing()
        {
            var testingServiceLocator = new TestingServiceLocator(null, new Mock<ISectionRepository>().Object, new Mock<IContentCacheState>().Object);

            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(testingServiceLocator);

            var blade = new ContentCachingBlade();
            blade.Spin(rotorContextFake.Object);

            Assert.IsNull(testingServiceLocator.PageRepositoryRegisteredByMethod);
        }

        public class TestingServiceLocator : IServiceLocator
        {
            private readonly IPageRepository pageRepository;
            private readonly ISectionRepository sectionRepository;
            private readonly IContentCacheState contentCacheState;

            public IPageRepository PageRepositoryRegisteredByMethod { get; private set; }
            public ISectionRepository SectionRepositoryRegisteredByMethod { get; private set; }

            public TestingServiceLocator(IPageRepository pageRepository, ISectionRepository sectionRepository, IContentCacheState contentCacheState)
            {
                this.pageRepository = pageRepository;
                this.sectionRepository = sectionRepository;
                this.contentCacheState = contentCacheState;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                object item = null;

                if (typeof (T) == typeof (IPageRepository))
                    item = pageRepository;
                if (typeof (T) == typeof (IContentCacheState))
                    item = contentCacheState;
                if (typeof (T) == typeof (ISectionRepository))
                    item = sectionRepository;

                if (item == null)
                    throw new ServiceResolutionException(typeof (T));

                return (T)item;
            }

            public T Resolve<T>(string key) where T : class
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>(Type type) where T : class
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type)
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

            public void Register<Interface>(Type implType) where Interface : class
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

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register(Type serviceType, Type implType)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Interface instance) where Interface : class
            {
                if (typeof (Interface) == typeof (IPageRepository))
                    PageRepositoryRegisteredByMethod = (IPageRepository)instance;

                if (typeof (Interface) == typeof (ISectionRepository))
                    SectionRepositoryRegisteredByMethod = (ISectionRepository)instance;
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
        }
    }
}