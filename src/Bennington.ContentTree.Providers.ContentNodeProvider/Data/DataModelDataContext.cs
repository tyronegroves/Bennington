using System.Collections.Generic;
using System.Linq;
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
		public const string PathToContentNodeProviderDraftXmlFileAppSettingsKey = "PathToContentNodeProviderDraftXmlFile";
		public const string PathToContentNodeProviderPublishedVersionXmlFileAppSettingsKey = "PathToContentNodeProviderPublishedVersionXmlFile";
		private static readonly object _lockObject = "lock";
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;
		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;

		public DataModelDataContext(IXmlFileSerializationHelper xmlFileSerializationHelper,
									IApplicationSettingsValueGetter applicationSettingsValueGetter)
		{
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
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
			return applicationSettingsValueGetter.GetValue(PathToContentNodeProviderPublishedVersionXmlFileAppSettingsKey);
		}

		private string GetPathToDraftVersionXmlFile()
		{
			return applicationSettingsValueGetter.GetValue(PathToContentNodeProviderDraftXmlFileAppSettingsKey);
		}
	}
}
