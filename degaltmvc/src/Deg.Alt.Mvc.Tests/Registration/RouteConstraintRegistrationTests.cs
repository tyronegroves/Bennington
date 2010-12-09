using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Deg.Alt.Mvc.Registration;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTurbine.ComponentModel;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc;

namespace Deg.Alt.Mvc.Tests.Registration
{
    [TestClass]
    public class RouteConstraintRegistrationTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Registers_GetContentItemForPageLocationAction()
        {
            VerifyThatThisWasRegisteredToThat(typeof(IContentPageInSectionRouteConstraint), typeof(ContentPageInSectionRouteConstraint));
        }

        [TestMethod]
        public void Registers_PageInRootRouteConstraint()
        {
            VerifyThatThisWasRegisteredToThat(typeof(IPageInRootRouteConstraint), typeof(PageInRootRouteConstraint));
        }

        private void VerifyThatThisWasRegisteredToThat(Type @this, Type that)
        {
            var serviceLocator = new TestServiceLocator();

            var action = mocker.Resolve<RouteConstraintRegistration>();
            action.Register(serviceLocator);

            Assert.AreEqual(that, serviceLocator.RegistrationDictionary[@this]);
        }

        private class TestServiceLocator : IServiceLocator
        {
            public Dictionary<Type, Type> RegistrationDictionary { get; private set; }

            public TestServiceLocator()
            {
                RegistrationDictionary = new Dictionary<Type, Type>();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

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
                RegistrationDictionary[serviceType] = implType;
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
        }
    }
}
