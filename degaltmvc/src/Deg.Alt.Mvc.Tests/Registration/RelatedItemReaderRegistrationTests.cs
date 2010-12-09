using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Deg.Alt.ContentProvider.RelatedItemReaders;
using Deg.Alt.Mvc.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Registration
{
    [TestClass]
    public class RelatedItemReaderRegistrationTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Registers_ContentItemReader()
        {
            VerifyThatThisWasRegisteredToThat(typeof(IRelatedItemReader), typeof(ContentItemReader), "Deg.Alt.ContentProvider.RelatedItemReaders.ContentItemReader");
        }

        [TestMethod]
        public void Registers_EmailItemReader()
        {
            VerifyThatThisWasRegisteredToThat(typeof(IRelatedItemReader), typeof(EmailItemReader), "Deg.Alt.ContentProvider.RelatedItemReaders.EmailItemReader");
        }

        [TestMethod]
        public void Registers_RelatedItemReader()
        {
            VerifyThatThisWasRegisteredToThat(typeof(IRelatedItemReader), typeof(MetadataItemReader), "Deg.Alt.ContentProvider.RelatedItemReaders.MetadataItemReader");
        }

        private void VerifyThatThisWasRegisteredToThat(Type @this, Type that, string name)
        {
            var serviceLocator = new TestServiceLocator();

            var action = mocker.Resolve<RelatedItemReaderRegistration>();
            action.Register(serviceLocator);

            Assert.IsTrue(serviceLocator.Registrations
                              .Where(x => x.From == @this)
                              .Where(x => x.To == that)
                              .Where(x => x.Name == name)
                              .Any());
        }

        public class Mapping
        {
            public Type From { get; set; }
            public Type To { get; set; }
            public string Name { get; set; }
        }

        private class TestServiceLocator : IServiceLocator
        {
            public IList<Mapping> Registrations { get; private set; }

            public TestServiceLocator()
            {
                Registrations = new List<Mapping>();
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
                Registrations.Add(new Mapping{From = typeof (Interface), To = typeof (Implementation), Name = key});
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
    }
}