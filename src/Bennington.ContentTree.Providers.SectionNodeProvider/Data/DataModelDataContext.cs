using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bennington.ContentTree.Helpers;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Data
{
	public interface IDataModelDataContext
	{
		IEnumerable<SectionNodeProviderDraft> GetAllSectionNodeProviderDrafts();
		void Create(SectionNodeProviderDraft instance);
		void Update(SectionNodeProviderDraft instance);
		void Delete(SectionNodeProviderDraft instance);
	}

	public class DataModelDataContext : IDataModelDataContext
	{
		public const string PathToSectionNodeProviderXmlFileAppSettingsKey = "PathToSectionNodeProviderXmlFile";
		private static readonly object _lockObject = "lock";
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;
		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
		private IGetPathToDataDirectoryService getPathToDataDirectoryService;

		public DataModelDataContext(IXmlFileSerializationHelper xmlFileSerializationHelper, 
									IGetPathToDataDirectoryService getPathToDataDirectoryService)
		{
			this.getPathToDataDirectoryService = getPathToDataDirectoryService;
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
		}

		public IEnumerable<SectionNodeProviderDraft> GetAllSectionNodeProviderDrafts()
		{
			lock(_lockObject)
			{
				var sectionNodeProviderDrafts = xmlFileSerializationHelper.DeserializeListFromPath<SectionNodeProviderDraft>(GetPathToSectionNodeXmlFile());
				return sectionNodeProviderDrafts;
			}
		}

		public void Create(SectionNodeProviderDraft instance)
		{
			lock(_lockObject)
			{
				var data = xmlFileSerializationHelper.DeserializeListFromPath<SectionNodeProviderDraft>(GetPathToSectionNodeXmlFile());
				data.Add(instance);
				xmlFileSerializationHelper.SerializeListToPath(data, GetPathToSectionNodeXmlFile());				
			}
		}

		public void Update(SectionNodeProviderDraft instance)
		{
			lock(_lockObject)
			{
				var data = xmlFileSerializationHelper.DeserializeListFromPath<SectionNodeProviderDraft>(GetPathToSectionNodeXmlFile());
				data.Remove(data.Where(a => a.SectionId == instance.SectionId).FirstOrDefault());
				data.Add(instance);
				xmlFileSerializationHelper.SerializeListToPath(data, GetPathToSectionNodeXmlFile());				
			}
		}

		public void Delete(SectionNodeProviderDraft instance)
		{
			lock(_lockObject)
			{
				var data = xmlFileSerializationHelper.DeserializeListFromPath<SectionNodeProviderDraft>(GetPathToSectionNodeXmlFile());
				data.Remove(data.Where(a => a.SectionId == instance.SectionId).FirstOrDefault());
				xmlFileSerializationHelper.SerializeListToPath(data, GetPathToSectionNodeXmlFile());
			}
		}

		private string GetPathToSectionNodeXmlFile()
		{
			return getPathToDataDirectoryService.GetPathToDirectory() + Path.DirectorySeparatorChar + "SectionNodeProviderDrafts.xml";
		}
	}
}
