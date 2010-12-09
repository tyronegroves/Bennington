using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Deg.Alt.Mvc.Registration;
using Deg.Alt.Mvc.Routing;
using Deg.Alt.Mvc.Routing.RouteCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Registration
{
    [TestClass]
    public class RouteCreatorRegistrationTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Register_Called_RegistersContentPageInSectionRouteCreator()
        {
            // arrange
            var serviceLocator = new TestServiceLocator();

            // act
            mocker.Resolve<RouteCreatorRegistration>()
                .Register(serviceLocator);

            var expectedRegistrations = serviceLocator.Registrations
                .Where(x => x.GetType() == typeof (TestServiceLocator.Registration<IRouteCreator, ContentPageInSectionRouteCreator>))
                .Where(x => x.Key == "ContentPageInSectionRouteCreator");

            // assert
            Assert.AreEqual(1, expectedRegistrations.Count());
        }

        [TestMethod]
        public void Register_Called_RegistersPageInRootRouteCreator()
        {
            // arrange
            var serviceLocator = new TestServiceLocator();

            // act
            mocker.Resolve<RouteCreatorRegistration>()
                .Register(serviceLocator);

            var expectedRegistrations = serviceLocator.Registrations
                .Where(x => x.GetType() == typeof (TestServiceLocator.Registration<IRouteCreator, PageInRootRouteCreator>))
                .Where(x => x.Key == "PageInRootRouteCreator");

            // assert
            Assert.AreEqual(1, expectedRegistrations.Count());
        }

        [TestMethod]
        public void Register_Called_RegistersRootPageRouteCreator()
        {
            // arrange
            var serviceLocator = new TestServiceLocator();

            // act
            mocker.Resolve<RouteCreatorRegistration>()
                .Register(serviceLocator);

            var expectedRegistrations = serviceLocator.Registrations
                .Where(x => x.GetType() == typeof (TestServiceLocator.Registration<IRouteCreator, RootPageRouteCreator>))
                .Where(x => x.Key == "RootPageRouteCreator");

            // assert
            Assert.AreEqual(1, expectedRegistrations.Count());
        }
    }

    public class TestServiceLocator : IServiceLocator
    {
        public TestServiceLocator()
        {
            Registrations = new List<RegistrationBase>();
        }

        public IList<RegistrationBase> Registrations { get; private set; }

        public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
        {
            Registrations.Add(new Registration<Interface, Implementation>{Key = key});
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IServiceLocator

        public T Resolve<T>() where T : class
        {
            throw new NotImplementedException();
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

        #endregion

        public class Registration<Interface, Implementation> : RegistrationBase
        {
        }
    }

    public class RegistrationBase
    {
        public string Key { get; set; }
    }
}