using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Domain.Events.Page;
using Paragon.ContentTree.Domain.Events.TreeNode;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.ContentNodeProvider.Denormalizers
{
	public class ContentNodeProviderDraftDenormalizer : IHandleDomainEvents<PageCreatedEvent>,
														IHandleDomainEvents<PageDeletedEvent>,
														IHandleDomainEvents<PageTreeNodeIdSetEvent>,
														IHandleDomainEvents<PageNameSetEvent>,
														IHandleDomainEvents<PageActionSetEvent>,
														IHandleDomainEvents<MetaTitleSetEvent>,
														IHandleDomainEvents<MetaDescriptionSetEvent>,
														IHandleDomainEvents<HeaderTextSetEvent>,
														IHandleDomainEvents<BodySetEvent>,
														IHandleDomainEvents<PageUrlSegmentSetEvent>,
														IHandleDomainEvents<PageSequenceSetEvent>
	{
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;

		public ContentNodeProviderDraftDenormalizer(IContentNodeProviderDraftRepository contentNodeProviderDraftRepository)
		{
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
			contentNodeProviderDraft.UrlSegment = domainEvent.UrlSegment;
			contentNodeProviderDraftRepository.Update(contentNodeProviderDraft);
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
	}
}