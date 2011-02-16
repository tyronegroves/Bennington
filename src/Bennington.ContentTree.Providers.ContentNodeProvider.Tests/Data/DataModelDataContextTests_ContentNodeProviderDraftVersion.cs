using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Data
{
	[TestClass]
	public class DataModelDataContextTests_ContentNodeProviderDraft
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			mocker.GetMock<IGetPathToDataDirectoryService>().Setup(a => a.GetPathToDirectory()).Returns("path");
			mocker.GetMock<IXmlFileSerializationHelper>().Setup(a => a.DeserializeListFromPath<ContentNodeProviderDraft>("path\\ContentNodeProviderDrafts.xml"))
				.Returns(new List<ContentNodeProviderDraft>()
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = "1",
				         			},
							});

		}

		[TestMethod]
		public void ContentNodeProviderDrafts_property_returns_ContentNodeProviderDrafts_from_xml_list()
		{
			var contentNodeProviderDrafts = mocker.Resolve<DataModelDataContext>().ContentNodeProviderDrafts;

			Assert.AreEqual("1", contentNodeProviderDrafts.First().PageId);
		}

		[TestMethod]
		public void Create_method_adds_a_ContentNodeProviderDraft_to_existing_list()
		{
			mocker.Resolve<DataModelDataContext>().Create(new ContentNodeProviderDraft()
			{
				PageId = "2",
			});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeListToPath(It.Is<List<ContentNodeProviderDraft>>(b => b.Where(c => c.PageId == "1").Count() == 1
																					&& b.Where(c => c.PageId == "2").Count() == 1
																					&& b.Count == 2), It.IsAny<string>()), Times.Once());
		}

		[TestMethod]
		public void Update_method_updates_an_existing_ContentNodeProviderDraft_to_existing_list()
		{
			mocker.Resolve<DataModelDataContext>().Update(new ContentNodeProviderDraft()
			{
				PageId = "1",
				Action = "test"
			});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeListToPath(It.Is<List<ContentNodeProviderDraft>>(b => b.Where(c => c.PageId == "1" && c.Action == "test").Count() == 1
																					&& b.Count == 1), It.IsAny<string>()), Times.Once());
		}

		[TestMethod]
		public void Delete_method_existing_ContentNodeProviderDraft_from_list()
		{
			mocker.Resolve<DataModelDataContext>().Delete(new ContentNodeProviderDraft()
			                                              	{
			                                              		PageId = "1"
			                                              	});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeListToPath(It.Is<List<ContentNodeProviderDraft>>(b => b.Count == 0), It.IsAny<string>()), Times.Once());
		}
	}
}
