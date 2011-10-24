using System;
using System.Linq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Bennington.Core.Helpers;
using Bennington.Core.SisoDb;
using SimpleCqrs.Eventing;
using SisoDb;
using SisoDb.Sql2008;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers
{
    public class ContentNodeProviderDraftDenormalizer : DatabaseFactory, IHandleDomainEvents<PageCreatedEvent>,
														IHandleDomainEvents<PageDeletedEvent>,
														IHandleDomainEvents<PageTreeNodeIdSetEvent>,
														IHandleDomainEvents<PageNameSetEvent>,
														IHandleDomainEvents<PageActionSetEvent>,
														IHandleDomainEvents<MetaTitleSetEvent>,
														IHandleDomainEvents<MetaDescriptionSetEvent>,
														IHandleDomainEvents<HeaderTextSetEvent>,
														IHandleDomainEvents<PageHeaderImageSetEvent>,
														IHandleDomainEvents<BodySetEvent>,
														IHandleDomainEvents<PageUrlSegmentSetEvent>,
														IHandleDomainEvents<PageSequenceSetEvent>,
														IHandleDomainEvents<PageHiddenSetEvent>,
														IHandleDomainEvents<PageInactiveSetEvent>
	{
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;
		private readonly ITreeNodeProviderContext treeNodeProviderContext;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly IApplicationSettingsValueGetter applicationSettingsValueGetter;
		private readonly IFileSystem fileSystem;

		public ContentNodeProviderDraftDenormalizer(IContentNodeProviderDraftRepository contentNodeProviderDraftRepository,
													ITreeNodeProviderContext treeNodeProviderContext,
													ITreeNodeSummaryContext treeNodeSummaryContext,
													IApplicationSettingsValueGetter applicationSettingsValueGetter,
													IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			this.applicationSettingsValueGetter = applicationSettingsValueGetter;
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.treeNodeProviderContext = treeNodeProviderContext;
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
		}

		public void Handle(PageCreatedEvent domainEvent)
		{
            var page = new ContentNodeProviderDraft
            {
                PageId = domainEvent.AggregateRootId.ToString()
            };

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                unitOfWork.Insert(page);
                unitOfWork.Commit();
            }

		}

		private ContentNodeProviderDraft GetContentNodeProviderDraft(DomainEvent domainEvent)
		{
            using (var queryEngine = database.CreateQueryEngine())
            {
                return queryEngine.Where<ContentNodeProviderDraft>(x => x.PageId == domainEvent.AggregateRootId.ToString()).First();
            }
		}

		public void Handle(PageNameSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.Name = domainEvent.Name;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
		}

		public void Handle(PageActionSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.Action = domainEvent.Action;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
		}

		public void Handle(MetaTitleSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.MetaTitle = domainEvent.MetaTitle;

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.MetaTitle = domainEvent.MetaTitle;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
			
		}

		public void Handle(MetaDescriptionSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.MetaDescription = domainEvent.MetaDescription;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
		}

		public void Handle(PageUrlSegmentSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			var treeNodeSummary = treeNodeSummaryContext.GetTreeNodeSummaryByTreeNodeId(contentNodeProviderDraft.TreeNodeId);
			
			contentNodeProviderDraft.UrlSegment = domainEvent.UrlSegment;

		    if (treeNodeSummary == null) return;
		    if (treeNodeSummary.UrlSegment == domainEvent.UrlSegment) return;
		    
            var provider = treeNodeProviderContext.GetProviderByTypeName(treeNodeSummary.Type);
		    provider.RegisterRouteForTreeNodeId(treeNodeSummary.Id);
		}

		public void Handle(HeaderTextSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.HeaderText = domainEvent.HeaderText;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
		}

		public void Handle(BodySetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.Body = domainEvent.Body;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
		}

		public void Handle(PageSequenceSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
            contentNodeProviderDraft.Sequence = domainEvent.PageSequence;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
        }

		public void Handle(PageDeletedEvent domainEvent)
		{
			foreach (var contentNodeProviderDraft in contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts().Where(a => a.TreeNodeId == domainEvent.TreeNodeId.ToString()))
			{
				contentNodeProviderDraftRepository.Delete(contentNodeProviderDraft);
			}
		}

		public void Handle(PageTreeNodeIdSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.TreeNodeId = domainEvent.TreeNodeId.ToString();
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
		}

		public void Handle(PageHiddenSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.Hidden = domainEvent.Hidden;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
        }

		public void Handle(PageInactiveSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.Inactive = domainEvent.Inactive;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }
        }

		public void Handle(PageHeaderImageSetEvent domainEvent)
		{

			var providerUploadPath = applicationSettingsValueGetter.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath");
			var draftFileUploadPath = applicationSettingsValueGetter.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath");
			var headerImageUploadPath = string.Format(@"{0}{1}\HeaderImage", draftFileUploadPath, domainEvent.AggregateRootId);
			if (!fileSystem.DirectoryExists(headerImageUploadPath))
			{
				fileSystem.CreateFolder(headerImageUploadPath);
			}

			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			try
			{
				fileSystem.DeleteFile(string.Format(@"{0}{1}\HeaderImage\{2}", draftFileUploadPath, domainEvent.AggregateRootId, contentNodeProviderDraft.HeaderImage));
			}
			catch (Exception) { }
			
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
            
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                contentNodeProviderDraft.HeaderImage = domainEvent.HeaderImage;
                unitOfWork.Update(contentNodeProviderDraft);
                unitOfWork.Commit();
            }

			if (string.IsNullOrEmpty(domainEvent.HeaderImage)) return;
			fileSystem.Copy(string.Format(@"{0}{1}\{3}\HeaderImage\{2}", providerUploadPath, contentNodeProviderDraft.TreeNodeId, domainEvent.HeaderImage, contentNodeProviderDraft.Action), 
							string.Format(@"{0}{1}\HeaderImage\{2}", draftFileUploadPath, domainEvent.AggregateRootId, domainEvent.HeaderImage));
		}
	}
}