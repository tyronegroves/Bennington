using System.Collections.Generic;
using System.Reflection;
using Bennington.Core.Registration;
using Bennington.Core.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Core.Tests
{
    [TestClass]
    public class InterfaceImplementationRegistratorTests
    {
        private Mock<MockServiceLocator> mockServiceLocator;

        [TestInitialize]
        public void SetupMocksForAllTests()
        {
            mockServiceLocator = new Mock<MockServiceLocator>();
        }

        [TestMethod]
        public void ClassThatImplementsInterfaceWithTwoImplementersIsNotRegistered()
        {
            var registrator = CreateInterfaceImplementationRegistrator();
            var serviceLocator = mockServiceLocator.Object;

            registrator.Register(serviceLocator);

            mockServiceLocator.Verify(
                locator => locator.Register(typeof(InterfaceWithTwoImplementers), typeof(ClassThatImplementsInterfaceWithTwoImplementers)), Times.Never());
        }

        [TestMethod]
        public void ClassThatImplementsInterfaceWithOneImplementerIsRegistered()
        {
            var registrator = CreateInterfaceImplementationRegistrator();
            var serviceLocator = mockServiceLocator.Object;

            registrator.Register(serviceLocator);

            mockServiceLocator.Verify(locator => locator.Register(typeof(InterfaceWithOneImplementer), typeof(ClassThatImplementsInterfaceWithOneImplementer)),
                                      Times.Once());
        }

        [TestMethod]
        public void InterfaceWithOneImplementerIsNotRegisteredWithItsSelf()
        {
            var registrator = CreateInterfaceImplementationRegistrator();
            var serviceLocator = mockServiceLocator.Object;

            registrator.Register(serviceLocator);

            mockServiceLocator.Verify(locator => locator.Register(typeof(InterfaceWithOneImplementer), typeof(InterfaceWithOneImplementer)), Times.Never());
        }

        [TestMethod]
        public void AbstractClassThatImplementsInterfaceWithOneImplementerIsNotRegistered()
        {
            var registrator = CreateInterfaceImplementationRegistrator();
            var serviceLocator = mockServiceLocator.Object;

            registrator.Register(serviceLocator);

            mockServiceLocator.Verify(
                locator => locator.Register(typeof(InterfaceWithOneAbstractImplementer), typeof(AbstractClassThatImplementsInterfaceWithOneAbstractImplementer)),
                Times.Never());
        }

        [TestMethod]
        public void InterfaceThatImplementsInterfaceWithOneImplementerIsNotRegistered()
        {
            var registrator = CreateInterfaceImplementationRegistrator();
            var serviceLocator = mockServiceLocator.Object;

            registrator.Register(serviceLocator);

            mockServiceLocator.Verify(
                locator => locator.Register(typeof(InterfaceWithOneInterfaceImplementer), typeof(InterfaceThatImplementsInterfaceWithOneInterfaceImplementer)),
                Times.Never());
        }

        private static InterfaceToSingleImplementationRegistrationConvention CreateInterfaceImplementationRegistrator()
        {
            return new TestableInterfaceToSingleImplementationRegistrationConvention();
        }
    }

    public class TestableInterfaceToSingleImplementationRegistrationConvention : InterfaceToSingleImplementationRegistrationConvention
    {
        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] {Assembly.GetExecutingAssembly()};
        }
    }
}