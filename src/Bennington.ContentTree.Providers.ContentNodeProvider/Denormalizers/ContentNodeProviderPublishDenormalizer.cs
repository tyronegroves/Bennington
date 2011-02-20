using System.Linq;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Bennington.Core.Helpers;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers
{
	public class ContentNodeProviderPublishDenormalizer : IHandleDomainEvents<PagePublishedEvent>,
															IHandleDomainEvents<PageDeletedEvent>
	{
		private readonly IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository;
		private readonly IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper;
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;
		private IApplicationSettingsValueGetter applicationSettingsValueGetter;
		private IFileSystem fileSystem;

		public ContentNodeProviderPublishDenormalizer(IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository,
			IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper,
			IContentNodeProviderDraftRepository contentNodeProviderDraftRepository,
			IApplicationSettingsValueGetter applicationSettingsValueGetter,
			IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
			this.contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper = contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper;
			this.contentNodeProviderPublishedVersionRepository = contentNodeProviderPublishedVersionRepository;
		}

		public void Handle(PagePublishedEvent domainEvent)
		{
			var draftVersion = contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts().Where(a => a.PageId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();
			if (draftVersion == null) return;

			var existingPublishedVersion = contentNodeProviderPublishedVersionRepository.GetAllContentNodeProviderPublishedVersions()
				.Where(a => a.PageId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();

			var draftFileUploadPath = applicationSettingsValueGetter.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath");
			var publishedVersionFileUploadPath = applicationSettingsValueGetter.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.PublishedVersionFileUploadPath");
			var draftVersionHeaderImagePath = string.Format(@"{0}{1}\HeaderImage\{2}", draftFileUploadPath, domainEvent.AggregateRootId, draftVersion.HeaderImage);
			if (!fileSystem.DirectoryExists(publishedVersionFileUploadPath + domainEvent.AggregateRootId + @"\HeaderImage"))
				fileSystem.CreateFolder(publishedVersionFileUploadPath + domainEvent.AggregateRootId + @"\HeaderImage");
			if (fileSystem.FileExists(draftVersionHeaderImagePath))
				fileSystem.Copy(draftVersionHeaderImagePath,
								string.Format(@"{0}{1}\HeaderImage\{2}", publishedVersionFileUploadPath, domainEvent.AggregateRootId, draftVersion.HeaderImage));

			if (existingPublishedVersion == null)
				contentNodeProviderPublishedVersionRepository
					.Create(contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper.CreateInstance(draftVersion));
			else
			{
				var mappedInstance = contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper.CreateInstance(draftVersion);
				contentNodeProviderPublishedVersionRepository.Update(mappedInstance);
			}
		}

		public void Handle(PageDeletedEvent domainEvent)
		{
			var item = contentNodeProviderPublishedVersionRepository
							.GetAllContentNodeProviderPublishedVersions()
								.Where(a => a.TreeNodeId == domainEvent.TreeNodeId.ToString()).FirstOrDefault();

			if (item != null)
				contentNodeProviderPublishedVersionRepository.Delete(item);
		}
	}
}
