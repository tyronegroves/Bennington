using System;
using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.Mvc.Blades;
using Deg.Alt.Mvc.Helpers;
using Deg.Alt.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Blades
{
    [TestClass]
    public class DefaultRegistrationBladeTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Registers_the_empty_controllers_to_exclude_from_registration_on_setup_when_resolving_the_type_throws()
        {
            var serviceLocator = new TestServiceLocator();
            serviceLocator.ThrowWhenResolvingThisType(typeof (IControllersExcludedFromRoutingRegistry));

            new DefaultRegistrationBlade(serviceLocator);

            Assert.AreEqual(typeof (EmptyControllersExcludedFromRoutingRegistry), serviceLocator.RegisteredTypes[typeof (IControllersExcludedFromRoutingRegistry)]);
        }

        [TestMethod]
        public void Does_not_register_the_empty_controllers_to_exclude_from_registration_on_setup_when_there_is_no_error_resolving_the_type()
        {
            var serviceLocator = new TestServiceLocator();

            new DefaultRegistrationBlade(serviceLocator);

            Assert.IsFalse(serviceLocator.RegisteredTypes.ContainsKey(typeof (IControllersExcludedFromRoutingRegistry)));
        }

        #region test service locator

        public class TestServiceLocator : IServiceLocator
        {
            public IDictionary<Type, Type> RegisteredTypes { get; set; }

            private readonly IList<Type> typesToThrowWhenResolving;

            public TestServiceLocator()
            {
                RegisteredTypes = new Dictionary<Type, Type>();
                typesToThrowWhenResolving = new List<Type>();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void ThrowWhenResolvingThisType(Type type)
            {
                typesToThrowWhenResolving.Add(type);
            }

            public T Resolve<T>() where T : class
            {
                if (typesToThrowWhenResolving.Contains(typeof (T)))
                    throw new Exception();
                return null;
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
                RegisteredTypes.Add(typeof (Interface), typeof (Implementation));
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

        #endregion
    }
}