using System;
using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.Mvc.Mappers;
using Deg.Alt.Mvc.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Registration
{
    [TestClass]
    public class MapperRegistrationTests
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
            VerifyThatThisWasRegisteredToThat(typeof (IRouteValueDictionaryToPageLocationMapper), typeof (RouteValueDictionaryToPageLocationMapper));
        }

        [TestMethod]
        public void Registers_EmailToMailMessageMapper()
        {
            VerifyThatThisWasRegisteredToThat(typeof(IEmailToMailMessageMapper), typeof(EmailToMailMessageMapper));
        }

        private void VerifyThatThisWasRegisteredToThat(Type @this, Type that)
        {
            var serviceLocator = new TestServiceLocator();

            var action = mocker.Resolve<MapperRegistration>();
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