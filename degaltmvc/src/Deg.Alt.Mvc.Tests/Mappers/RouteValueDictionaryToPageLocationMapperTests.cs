using System.Web.Routing;
using AutoMapperAssist;
using AutoMoq;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Deg.Alt.Mvc.Tests.Mappers
{
    [TestClass]
    public class RouteValueDictionaryToPageLocationMapperTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Constructor_Instantiated_ConfigurationIsValid()
        {
            mocker.Resolve<RouteValueDictionaryToPageLocationMapper>().AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Map_SectionIdIsTest_SetsSectionIdToTest()
        {
            // arrange
            var mapper = mocker.Resolve<RouteValueDictionaryToPageLocationMapper>();

            // act
            var result = mapper.CreateInstance(new RouteValueDictionary{{"sectionId", "TEST"}});

            // assert
            Assert.AreEqual("TEST", result.SectionId);
        }

        [TestMethod]
        public void Map_ActionIsTest_SetsStepToTest()
        {
            // arrange
            var mapper = mocker.Resolve<RouteValueDictionaryToPageLocationMapper>();

            // act
            var result = mapper.CreateInstance(new RouteValueDictionary{{"action", "TEST"}});

            // assert
            Assert.AreEqual("TEST", result.Step);
        }

        [TestMethod]
        public void The_results_from_the_get_controller_action_is_returns_as_the_controller()
        {
            ReturnThisControllerWhenThisPageIdIsProvided("TEST", "Expected controller");

            var dictionary = new RouteValueDictionary{{"controller", "TEST"}};
            var result = GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("Expected controller", result.Controller);
        }

        [TestMethod]
        public void When_the_controller_value_is_test_then_the_page_id_should_be_test()
        {
            var result = GetThePageLocationForThisRouteDictionary(new RouteValueDictionary{{"controller", "TEST"}});
            Assert.AreEqual("TEST", result.PageId);
        }

        [TestMethod]
        public void Sets_the_pageid_property_in_the_dictionary_to_match_the_controller_value()
        {
            var dictionary = new RouteValueDictionary{{"controller", "TEST"}};
            GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("TEST", dictionary["pageId"]);
        }

        [TestMethod]
        public void The_results_from_the_get_controller_action_is_set_in_the_route_value_dictionary()
        {
            ReturnThisControllerWhenThisPageIdIsProvided("TEST", "Expected controller");

            var dictionary = new RouteValueDictionary{{"controller", "TEST"}};
            GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("Expected controller", dictionary["controller"]);
        }

        [TestMethod]
        public void Marks_the_route_value_dictionary_as_fixed_after_it_is_passed_through_the_mapper()
        {
            var dictionary = new RouteValueDictionary();
            GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("1", dictionary["fixed"]);
        }

        [TestMethod]
        public void When_the_route_value_dictionary_is_marked_as_fixed_then_the_controller_is_not_altered_in_the_dictionary()
        {
            var dictionary = new RouteValueDictionary{
                                                         {"controller", "should not be altered"},
                                                         {"Fixed", "1"}
                                                     };
            GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("should not be altered", dictionary["controller"]);
        }

        [TestMethod]
        public void When_the_route_value_dictionary_is_marked_as_fixed_then_the_page_id_is_not_altered_in_the_dictionary()
        {
            var dictionary = new RouteValueDictionary{
                                                         {"pageId", "should not be altered"},
                                                         {"Fixed", "1"}
                                                     };
            GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("should not be altered", dictionary["pageId"]);
        }

        [TestMethod]
        public void When_the_route_value_dictionary_is_marked_as_fixed_then_set_the_pageId_to_the_value_in_the_dictionary()
        {
            var dictionary = new RouteValueDictionary{
                                                         {"pageId", "Expected pageId"},
                                                         {"controller", "don't want this value"},
                                                         {"Fixed", "1"}
                                                     };
            var result = GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("Expected pageId", result.PageId);
        }

        [TestMethod]
        public void When_the_route_value_dictionary_is_marked_as_fixed_then_set_the_controller_to_the_value_in_the_dictionary()
        {
            mocker.GetMock<IGetControllerForPageIdAction>()
                .Setup(x => x.GetController(It.IsAny<string>()))
                .Returns("don't want this value");

            var dictionary = new RouteValueDictionary{
                                                         {"controller", "Expected controller"},
                                                         {"Fixed", "1"}
                                                     };
            var result = GetThePageLocationForThisRouteDictionary(dictionary);
            Assert.AreEqual("Expected controller", result.Controller);
        }

        private void ReturnThisControllerWhenThisPageIdIsProvided(string pageId, string controller)
        {
            mocker.GetMock<IGetControllerForPageIdAction>()
                .Setup(x => x.GetController(pageId))
                .Returns(controller);
        }

        private PageLocation GetThePageLocationForThisRouteDictionary(RouteValueDictionary dictionary)
        {
            var mapper = CreateTheMapper();
            return mapper.CreateInstance(dictionary);
        }

        private RouteValueDictionaryToPageLocationMapper CreateTheMapper()
        {
            return mocker.Resolve<RouteValueDictionaryToPageLocationMapper>();
        }
    }
}