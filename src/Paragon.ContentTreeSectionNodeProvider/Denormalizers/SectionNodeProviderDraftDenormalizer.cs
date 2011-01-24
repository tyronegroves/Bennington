using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.Events.Section;
using Paragon.ContentTree.SectionNodeProvider.Data;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.SectionNodeProvider.Denormalizers
{
	public class SectionNodeProviderDraftDenormalizer : IHandleDomainEvents<SectionCreatedEvent>,
														IHandleDomainEvents<SectionDeletedEvent>,
														IHandleDomainEvents<SectionNameSetEvent>
	{
		private readonly IDataModelDataContext dataModelDataContext;

		public SectionNodeProviderDraftDenormalizer(IDataModelDataContext dataModelDataContext)
		{
			this.dataModelDataContext = dataModelDataContext;
		}

		public void Handle(SectionCreatedEvent domainEvent)
		{
			dataModelDataContext.Create(new SectionNodeProviderDraft()
			                            	{
			                            		SectionId = domainEvent.SectionId.ToString(),
			                            	});
		}

		public void Handle(SectionDeletedEvent domainEvent)
		{
			dataModelDataContext.Delete(new SectionNodeProviderDraft()
			                            	{
			                            		SectionId = domainEvent.AggregateRootId.ToString(),
			                            	});
		}

		public void Handle(SectionNameSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
			sectionNodeProviderDraft.Name = domainEvent.Name;
			dataModelDataContext.Update(sectionNodeProviderDraft);
		}

		private SectionNodeProviderDraft GetSectionNodeProviderDraftFromDomainEvent(DomainEvent domainEvent)
		{
			return dataModelDataContext.GetAllSectionNodeProviderDrafts().Where(a => a.SectionId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();
		}

		public void Handle(SectionSequenceSetEvent domainEvent)
		{
			var section = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
			section.Sequence = domainEvent.SectionSequence;
			dataModelDataContext.Update(section);
		}

		public void Handle(SectionUrlSegmentSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
			sectionNodeProviderDraft.UrlSegment = domainEvent.UrlSegment;
			dataModelDataContext.Update(sectionNodeProviderDraft);
		}

		public void Handle(SectionDefaultTreeNodeIdSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
			sectionNodeProviderDraft.DefaultTreeNodeId = domainEvent.DefaultTreeNodeId.ToString();
			dataModelDataContext.Update(sectionNodeProviderDraft);
		}
	}
}
