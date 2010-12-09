using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using AutoMoq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Tests.Routing.RouteConstraints
{
    [TestClass]
    public class SectionRouteConstraintTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Returns_true_when_there_is_a_section_that_matches_on_section_id()
        {
            mocker.GetMock<ISectionRepository>()
                .Setup(x => x.GetSections())
                .Returns(new[]{new Section{Id = "match"}});

            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["sectionId"] = "match";

            var constraint = new SectionRouteConstraint(new TestServiceLocator(mocker.GetMock<ISectionRepository>().Object));
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Returns_true_when_there_is_a_section_that_matches_on_section_id_with_case_insensitivity()
        {
            mocker.GetMock<ISectionRepository>()
                .Setup(x => x.GetSections())
                .Returns(new[]{new Section{Id = "match"}});

            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["sectionId"] = "MATCH";

            var constraint = new SectionRouteConstraint(new TestServiceLocator(mocker.GetMock<ISectionRepository>().Object));
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Returns_false_when_there_is_no_matching_section()
        {
            mocker.GetMock<ISectionRepository>()
                .Setup(x => x.GetSections())
                .Returns(new[]{new Section{Id = "match"}});

            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["sectionId"] = "will not match";

            var constraint = new SectionRouteConstraint(new TestServiceLocator(mocker.GetMock<ISectionRepository>().Object));
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, routeValueDictionary, RouteDirection.IncomingRequest);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Returns_false_when_there_is_no_section_id_in_the_route_value_dictionary()
        {
            var constraint = new SectionRouteConstraint(new TestServiceLocator(mocker.GetMock<ISectionRepository>().Object));
            var result = constraint.Match(mocker.GetMock<HttpContextBase>().Object, null, string.Empty, new RouteValueDictionary(), RouteDirection.IncomingRequest);

            Assert.IsFalse(result);
        }

        private class TestServiceLocator : IServiceLocator
        {
            private readonly ISectionRepository sectionRepository;

            public TestServiceLocator(ISectionRepository sectionRepository)
            {
                this.sectionRepository = sectionRepository;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                return sectionRepository as T;
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
    }
}