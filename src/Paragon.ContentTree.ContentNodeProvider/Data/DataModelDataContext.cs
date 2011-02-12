using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.Core.Helpers;

namespace Paragon.ContentTree.ContentNodeProvider.Data
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
			var contentNodeProviderPublishedVersions = GetContentNodeProviderPublishedVersionsFromXmlFile();
			contentNodeProviderPublishedVersions.Remove(contentNodeProviderPublishedVersions.Where(a => a.PageId == instance.PageId).FirstOrDefault());
			contentNodeProviderPublishedVersions.Add(instance);
			xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderPublishedVersions, GetPathToXmlFile());
		}

		public void Update(ContentNodeProviderDraft instance)
		{
			throw new NotImplementedException();
		}

		public void Delete(ContentNodeProviderDraft instance)
		{
			throw new NotImplementedException();
		}

		public void Delete(ContentNodeProviderPublishedVersion instance)
		{
			var contentNodeProviderPublishedVersions = GetContentNodeProviderPublishedVersionsFromXmlFile();
			contentNodeProviderPublishedVersions.Remove(contentNodeProviderPublishedVersions.Where(a => a.PageId == instance.PageId).FirstOrDefault());
			xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderPublishedVersions, GetPathToXmlFile());
		}

		public void Create(ContentNodeProviderPublishedVersion instance)
		{
			var contentNodeProviderPublishedVersions = GetContentNodeProviderPublishedVersionsFromXmlFile();
			contentNodeProviderPublishedVersions.Add(instance);
			xmlFileSerializationHelper.SerializeListToPath(contentNodeProviderPublishedVersions, GetPathToXmlFile());
		}

		private List<ContentNodeProviderPublishedVersion> GetContentNodeProviderPublishedVersionsFromXmlFile()
		{
			return xmlFileSerializationHelper.DeserializeListFromPath<ContentNodeProviderPublishedVersion>(GetPathToXmlFile());
		}

		public void Create(ContentNodeProviderDraft instance)
		{
			throw new NotImplementedException();
		}

		public IQueryable<ContentNodeProviderPublishedVersion> ContentNodeProviderPublishedVersions
		{
			get
			{
				return xmlFileSerializationHelper
							.DeserializeListFromPath<ContentNodeProviderPublishedVersion>(GetPathToXmlFile()).AsQueryable();
			}
		}

		private string GetPathToXmlFile()
		{
			return applicationSettingsValueGetter.GetValue(PathToContentNodeProviderPublishedVersionXmlFileAppSettingsKey);
		}

		public IQueryable<ContentNodeProviderDraft> ContentNodeProviderDrafts
		{
			get 
			{
				throw new NotImplementedException();
			}
		}
	}
}
