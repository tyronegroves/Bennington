﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bennington.ContentTree.Helpers;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		void Update(ContentNodeProviderPublishedVersion instance);
		void Update(ContentNodeProviderDraft instance);
		void Delete(ContentNodeProviderDraft instance);
		void Delete(ContentNodeProviderPublishedVersion instance);
		void Create(ContentNodeProviderPublishedVersion instance);
		void Create(ContentNodeProviderDraft instance);
		IQueryable<ContentNodeProviderPublishedVersion> ContentNodeProviderPublishedVersions { get; }
		IQueryable<ContentNodeProviderDraft> ContentNodeProviderDrafts { get; }
	}

	public class DataModelDataContext : IDataModelDataContext
	{
		private static readonly object _lockObject = "lock";
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;
		private readonly IGetPathToDataDirectoryService getPathToDataDirectoryService;

		public DataModelDataContext(IXmlFileSerializationHelper xmlFileSerializationHelper,
									IGetPathToDataDirectoryService getPathToDataDirectoryService)
		{
			this.getPathToDataDirectoryService = getPathToDataDirectoryService;
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
		}

		public void Update(ContentNodeProviderPublishedVersion instance)
		{
			lock(_lockObject)
			{
				var contentNodeProviderPublishedVersions = GetContentNodeProviderPublishedVersionsFromXmlFile();
				contentNodeProviderPublishedVersions.Remove(contentNodeProviderPublishedVersions.Where(a => a.PageId == instance.PageId).FirstOrDefault());
				contentNodeProviderPublishedVersions.Add(instance);
				xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderPublishedVersions, GetPathToPublishedVersionXmlFile());
			}
		}

		public void Update(ContentNodeProviderDraft instance)
		{
			lock(_lockObject)
			{
				var contentNodeProviderDrafts = xmlFileSerializationHelper.DeserializeListFromPath<ContentNodeProviderDraft>(GetPathToDraftVersionXmlFile());
				contentNodeProviderDrafts.Remove(contentNodeProviderDrafts.Where(a => a.PageId == instance.PageId).FirstOrDefault());
				contentNodeProviderDrafts.Add(instance);
				xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderDrafts, GetPathToDraftVersionXmlFile());
			}
		}

		public void Delete(ContentNodeProviderDraft instance)
		{
			lock(_lockObject)
			{
				var contentNodeProviderDrafts = xmlFileSerializationHelper.DeserializeListFromPath<ContentNodeProviderDraft>(GetPathToDraftVersionXmlFile());
				contentNodeProviderDrafts.Remove(contentNodeProviderDrafts.Where(a => a.PageId == instance.PageId).FirstOrDefault());
				xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderDrafts, GetPathToDraftVersionXmlFile());
			}
		}

		public void Delete(ContentNodeProviderPublishedVersion instance)
		{
			lock(_lockObject)
			{
				var contentNodeProviderPublishedVersions = GetContentNodeProviderPublishedVersionsFromXmlFile();
				contentNodeProviderPublishedVersions.Remove(contentNodeProviderPublishedVersions.Where(a => a.PageId == instance.PageId).FirstOrDefault());
				xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderPublishedVersions, GetPathToPublishedVersionXmlFile());				
			}
		}

		public void Create(ContentNodeProviderPublishedVersion instance)
		{
			lock(_lockObject)
			{
				var contentNodeProviderPublishedVersions = GetContentNodeProviderPublishedVersionsFromXmlFile();
				contentNodeProviderPublishedVersions.Add(instance);
				xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderPublishedVersions, GetPathToPublishedVersionXmlFile());				
			}
		}

		public IQueryable<ContentNodeProviderDraft> ContentNodeProviderDrafts
		{
			get
			{
				lock(_lockObject)
				{
					return xmlFileSerializationHelper
							.DeserializeListFromPath<ContentNodeProviderDraft>(GetPathToDraftVersionXmlFile()).AsQueryable();					
				}
			}
		}

		public void Create(ContentNodeProviderDraft instance)
		{
			lock(_lockObject)
			{
				var contentNodeProviderDrafts = xmlFileSerializationHelper.DeserializeListFromPath<ContentNodeProviderDraft>(GetPathToDraftVersionXmlFile());
				contentNodeProviderDrafts.Add(instance);
				xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderDrafts, GetPathToDraftVersionXmlFile());				
			}
		}

		public IQueryable<ContentNodeProviderPublishedVersion> ContentNodeProviderPublishedVersions
		{
			get
			{
				lock(_lockObject)
				{
					return xmlFileSerializationHelper
								.DeserializeListFromPath<ContentNodeProviderPublishedVersion>(GetPathToPublishedVersionXmlFile()).AsQueryable();
				}
			}
		}

		private List<ContentNodeProviderPublishedVersion> GetContentNodeProviderPublishedVersionsFromXmlFile()
		{
			return xmlFileSerializationHelper.DeserializeListFromPath<ContentNodeProviderPublishedVersion>(GetPathToPublishedVersionXmlFile());
		}

		private string GetPathToPublishedVersionXmlFile()
		{
			return getPathToDataDirectoryService.GetPathToDirectory() + Path.DirectorySeparatorChar + "ContentNodeProviderPublishedVersions.xml";
		}

		private string GetPathToDraftVersionXmlFile()
		{
			return getPathToDataDirectoryService.GetPathToDirectory() + Path.DirectorySeparatorChar + "ContentNodeProviderDrafts.xml";
		}
	}
}
