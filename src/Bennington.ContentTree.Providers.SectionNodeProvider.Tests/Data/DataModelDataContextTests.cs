using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Tests.Data
{
    [TestClass]
    public class DataModelDataContextTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Init()
        {
            mocker = new AutoMoqer();
            mocker.GetMock<IGetPathToDataDirectoryService>().Setup(a => a.GetPathToDirectory())
                .Returns("path");
            mocker.GetMock<IXmlFileSerializationHelper>().Setup(a => a.DeserializeListFromPath<SectionNodeProviderDraft>("path" + Path.DirectorySeparatorChar + "SectionNodeProviderDrafts.xml"))
                .Returns(new List<SectionNodeProviderDraft>()
                            {
                                new SectionNodeProviderDraft()
                                    {
                                        SectionId = "1",
                                    },
                            });

        }

        [TestMethod]
        public void GetAllSectionNodeProviderDrafts_method_returns_SectionNodeProviderDrafts_from_xml_list()
        {
            var sectionNodeProviderDrafts = mocker.Resolve<DataModelDataContext>().GetAllSectionNodeProviderDrafts();

            Assert.AreEqual("1", sectionNodeProviderDrafts.First().SectionId);
        }

        [TestMethod]
        public void Create_method_adds_a_SectionNodeProviderDraft_to_existing_list()
        {
            mocker.Resolve<DataModelDataContext>().Create(new SectionNodeProviderDraft()
            {
                SectionId = "2",
            });

            mocker.GetMock<IXmlFileSerializationHelper>()
                .Verify(a => a.SerializeListToPath(It.Is<List<SectionNodeProviderDraft>>(b => b.Where(c => c.SectionId == "1").Count() == 1
                                                                                    && b.Where(c => c.SectionId == "2").Count() == 1
                                                                                    && b.Count == 2), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void Update_method_updates_an_existing_SectionNodeProviderDraft_to_existing_list()
        {
            mocker.Resolve<DataModelDataContext>().Update(new SectionNodeProviderDraft()
            {
                SectionId = "1",
                Name = "test"
            });

            mocker.GetMock<IXmlFileSerializationHelper>()
                .Verify(a => a.SerializeListToPath(It.Is<List<SectionNodeProviderDraft>>(b => b.Where(c => c.SectionId == "1" && c.Name == "test").Count() == 1
                                                                                    && b.Count == 1), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void Delete_method_existing_SectionNodeProviderDraft_from_list()
        {
            mocker.Resolve<DataModelDataContext>().Delete(new SectionNodeProviderDraft()
            {
                SectionId = "1"
            });

            mocker.GetMock<IXmlFileSerializationHelper>()
                .Verify(a => a.SerializeListToPath(It.Is<List<SectionNodeProviderDraft>>(b => b.Count == 0), It.IsAny<string>()), Times.Once());
        }

    }
}
