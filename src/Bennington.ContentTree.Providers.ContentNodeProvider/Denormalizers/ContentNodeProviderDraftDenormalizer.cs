using System;
using System.Linq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers
{
	public class ContentNodeProviderDraftDenormalizer : IHandleDomainEvents<PageCreatedEvent>,
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

		public ContentNodeProviderDraftDenormalizer(IContentNodeProviderDraftRepository contentNodeProviderDraftRepository,
													ITreeNodeProviderContext treeNodeProviderContext,
													ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.treeNodeProviderContext = treeNodeProviderContext;
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
		}

		public void Handle(PageCreatedEvent domainEvent)
		{
			contentNodeProviderDraftRepository.Create(new ContentNodeProviderDraft()
			                                          	{
			                                          		PageId = domainEvent.AggregateRootId.ToString()
			                                          	});
		}

		private ContentNodeProviderDraft GetContentNodeProviderDraft(DomainEvent domainEvent)
		{
			return contentNodeProviderDraftRepository
						.GetAllContentNodeProviderDrafts().Where(a => a.PageId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();
		}

		public void Handle(PageNameSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.Name = domainEvent.Name;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(PageActionSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.Action = domainEvent.Action;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(MetaTitleSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.MetaTitle = domainEvent.MetaTitle;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(MetaDescriptionSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.MetaDescription = domainEvent.MetaDescription;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(PageUrlSegmentSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			var treeNodeSummary = treeNodeSummaryContext.GetTreeNodeSummaryByTreeNodeId(contentNodeProviderDraft.TreeNodeId);
			
			contentNodeProviderDraft.UrlSegment = domainEvent.UrlSegment;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);

			if (treeNodeSummary != null)
			{
				if (treeNodeSummary.UrlSegment != domainEvent.UrlSegment)
				{
					var provider = treeNodeProviderContext.GetProviderByTypeName(treeNodeSummary.Type);
					provider.RegisterRouteForTreeNodeId(treeNodeSummary.Id);
				}
			}
		}

		public void Handle(HeaderTextSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.HeaderText = domainEvent.HeaderText;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(BodySetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.Body = domainEvent.Body;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(PageSequenceSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.Sequence = domainEvent.PageSequence;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
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
			contentNodeProviderDraft.TreeNodeId = domainEvent.TreeNodeId.ToString();
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(PageHiddenSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.Hidden = domainEvent.Hidden;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(PageInactiveSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.Inactive = domainEvent.Inactive;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}

		public void Handle(PageHeaderImageSetEvent domainEvent)
		{
			var contentNodeProviderDraft = GetContentNodeProviderDraft(domainEvent);
			contentNodeProviderDraft.HeaderImage = domainEvent.HeaderImage;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
		}
	}
}