using System;
using System.Collections.Generic;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Blades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTurbine;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Blades
{
    [TestClass]
    public class InstinctContentBladeTests
    {
        private AutoMoqer mocker;
        private int OneForTheRegistryOfTheRegistry = 1;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Registers_mapping_when_one_default_mapping_exists()
        {
            var registeredTypes = RegisterTypes(new[]{
                                                         new TypeMapping(typeof (string), typeof (int))
                                                     });

            Assert.AreEqual(1, registeredTypes.Count);
            Assert.AreEqual(typeof (int), registeredTypes[typeof (string)]);
        }

        [TestMethod]
        public void Registers_two_mappings_when_two_mappings_exist()
        {
            var registeredTypes = RegisterTypes(new[]{
                                                         new TypeMapping(typeof (string), typeof (int)),
                                                         new TypeMapping(typeof (double), typeof (decimal))
                                                     });

            Assert.AreEqual(2, registeredTypes.Count);
            Assert.AreEqual(typeof (int), registeredTypes[typeof (string)]);
            Assert.AreEqual(typeof (decimal), registeredTypes[typeof (double)]);
        }

        [TestMethod]
        public void When_service_locator_thows_an_Exception_during_resolution_register_the_default_service_locator()
        {
            var serviceLocator = new TestingServiceLocator(null);
            AddRegistrationsWithThisServiceLocator(serviceLocator);

            Assert.AreEqual(typeof (DefaultMappingsRegistry), serviceLocator.RegisteredTypes[typeof (IMappingsRegistry)]);
        }

        private IDictionary<Type, Type> RegisterTypes(IEnumerable<TypeMapping> typeMappings)
        {
            var mappingsRegistryFake = CreateMappingsRegistryWithTheseMappings(typeMappings);

            var serviceLocator = new TestingServiceLocator(mappingsRegistryFake.Object);

            AddRegistrationsWithThisServiceLocator(serviceLocator);

            return serviceLocator.RegisteredTypes;
        }

        private static Mock<IMappingsRegistry> CreateMappingsRegistryWithTheseMappings(IEnumerable<TypeMapping> typeMappings)
        {
            var mappingsRegistryFake = new Mock<IMappingsRegistry>();
            mappingsRegistryFake.Setup(x => x.GetMappings())
                .Returns(typeMappings);
            return mappingsRegistryFake;
        }

        private void AddRegistrationsWithThisServiceLocator(TestingServiceLocator serviceLocator)
        {
            var blade = new InstinctContentBlade(serviceLocator);
            blade.AddRegistrations(null);
        }

        private static Mock<IRotorContext> CreateRotorContextWithThisServiceLocator(IServiceLocator serviceLocator)
        {
            var rotorContextFake = new Mock<IRotorContext>();
            rotorContextFake.Setup(x => x.ServiceLocator)
                .Returns(serviceLocator);
            return rotorContextFake;
        }

        #region TestingServiceLocator

        public class TestingServiceLocator : IServiceLocator
        {
            private IMappingsRegistry mappingsRegistry;

            public IDictionary<Type, Type> RegisteredTypes { get; set; }

            public TestingServiceLocator(IMappingsRegistry mappingsRegistry)
            {
                this.mappingsRegistry = mappingsRegistry;
                RegisteredTypes = new Dictionary<Type, Type>();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                if (typeof (T) == typeof (IMappingsRegistry))
                {
                    if (mappingsRegistry == null)
                        throw new ServiceResolutionException(typeof (IMappingsRegistry));
                    return (T)mappingsRegistry;
                }

                throw new NotImplementedException();
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
                RegisteredTypes.Add(typeof (Interface), typeof (Implementation));
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
                RegisterThisType(serviceType, implType);
            }

            private void RegisterThisType(Type serviceType, Type implType)
            {
                if (RegisteredTypes.ContainsKey(serviceType) == false)
                    RegisteredTypes.Add(serviceType, implType);

                if (serviceType == typeof (IMappingsRegistry) && implType == typeof (DefaultMappingsRegistry))
                    mappingsRegistry = new DefaultMappingsRegistry();
            }

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Type implType) where Interface : class
            {
                RegisterThisType(typeof (Interface), implType);
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