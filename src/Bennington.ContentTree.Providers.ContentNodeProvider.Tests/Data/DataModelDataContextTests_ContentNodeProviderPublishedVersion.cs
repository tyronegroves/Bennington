﻿using System.Collections.Generic;
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
	public class DataModelDataContextTests_ContentNodeProviderPublishedVersion
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			mocker.GetMock<IGetPathToDataDirectoryService>().Setup(a => a.GetPathToDirectory())
				.Returns("path");
			mocker.GetMock<IXmlFileSerializationHelper>().Setup(a => a.DeserializeListFromPath<ContentNodeProviderPublishedVersion>("path" + Path.DirectorySeparatorChar + "ContentNodeProviderPublishedVersions.xml"))
				.Returns(new List<ContentNodeProviderPublishedVersion>()
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
				         				PageId = "1",
				         			},
							});

		}

		[TestMethod]
		public void ContentNodeProviderPublishedVersions_property_returns_ContentNodeProviderPublishedVersions_from_xml_list()
		{
			var contentNodeProviderPublishedVersions = mocker.Resolve<DataModelDataContext>().ContentNodeProviderPublishedVersions;

			Assert.AreEqual("1", contentNodeProviderPublishedVersions.First().PageId);
		}

		[TestMethod]
		public void Create_method_adds_a_ContentNodeProviderPublishedVersion_to_existing_list()
		{
			mocker.Resolve<DataModelDataContext>().Create(new ContentNodeProviderPublishedVersion()
			{
				PageId = "2",
			});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeListToPath(It.Is<List<ContentNodeProviderPublishedVersion>>(b => b.Where(c => c.PageId == "1").Count() == 1
																					&& b.Where(c => c.PageId == "2").Count() == 1
																					&& b.Count == 2), It.IsAny<string>()), Times.Once());
		}

		[TestMethod]
		public void Update_method_updates_an_existing_ContentNodeProviderPublishedVersion_to_existing_list()
		{
			mocker.Resolve<DataModelDataContext>().Update(new ContentNodeProviderPublishedVersion()
			{
				PageId = "1",
				Action = "test"
			});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeListToPath(It.Is<List<ContentNodeProviderPublishedVersion>>(b => b.Where(c => c.PageId == "1" && c.Action == "test").Count() == 1
																					&& b.Count == 1), It.IsAny<string>()), Times.Once());
		}

		[TestMethod]
		public void Delete_method_existing_ContentNodeProviderPublishedVersion_from_list()
		{
			mocker.Resolve<DataModelDataContext>().Delete(new ContentNodeProviderPublishedVersion()
			                                              	{
			                                              		PageId = "1"
			                                              	});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeListToPath(It.Is<List<ContentNodeProviderPublishedVersion>>(b => b.Count == 0), It.IsAny<string>()), Times.Once());
		}
	}
}
