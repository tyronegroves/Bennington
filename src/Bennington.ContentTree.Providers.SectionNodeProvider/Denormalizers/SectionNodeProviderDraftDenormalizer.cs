using System.Linq;
using Bennington.ContentTree.Domain.Events.Section;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Denormalizers
{
	public class SectionNodeProviderDraftDenormalizer : IHandleDomainEvents<SectionCreatedEvent>,
														IHandleDomainEvents<SectionDeletedEvent>,
														IHandleDomainEvents<SectionNameSetEvent>,
														IHandleDomainEvents<SectionTreeNodeIdSetEvent>,
														IHandleDomainEvents<SectionUrlSegmentSetEvent>,
														IHandleDomainEvents<SectionSequenceSetEvent>,
														IHandleDomainEvents<SectionDefaultTreeNodeIdSetEvent>
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
			var sectionNodeProviderDraft = dataModelDataContext.GetAllSectionNodeProviderDrafts().Where(a => a.TreeNodeId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();
			dataModelDataContext.Delete(sectionNodeProviderDraft);
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

		public void Handle(SectionTreeNodeIdSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
			sectionNodeProviderDraft.TreeNodeId = domainEvent.TreeNodeId.ToString();
			dataModelDataContext.Update(sectionNodeProviderDraft);
		}
	}
}
