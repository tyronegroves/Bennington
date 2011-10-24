using System;
using System.Linq;
using Bennington.ContentTree.Domain.Events.Section;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using Bennington.Core.SisoDb;
using SimpleCqrs.Eventing;
using SisoDb;
using SisoDb.Sql2008;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Denormalizers
{
    public class SectionNodeProviderDraftDenormalizer : DatabaseFactory, IHandleDomainEvents<SectionCreatedEvent>,
														IHandleDomainEvents<SectionDeletedEvent>,
														IHandleDomainEvents<SectionNameSetEvent>,
														IHandleDomainEvents<SectionTreeNodeIdSetEvent>,
														IHandleDomainEvents<SectionUrlSegmentSetEvent>,
														IHandleDomainEvents<SectionSequenceSetEvent>,
														IHandleDomainEvents<SectionDefaultTreeNodeIdSetEvent>,
														IHandleDomainEvents<SectionHiddenSetEvent>,
														IHandleDomainEvents<SectionInactiveSetEvent>
	{
		public void Handle(SectionCreatedEvent domainEvent)
		{
		    var sectionnNode = new SectionNodeProviderDraft { SectionId = domainEvent.SectionId.ToString() };

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                unitOfWork.Insert(sectionnNode);
                unitOfWork.Commit();
            }
		}

		public void Handle(SectionDeletedEvent domainEvent)
		{
		    var sectionNodeProviderDraft = new SectionNodeProviderDraft();

            using (var queryEngine = database.CreateQueryEngine())
            {
                sectionNodeProviderDraft = queryEngine.Where<SectionNodeProviderDraft>(x => x.SectionId == domainEvent.AggregateRootId.ToString()).First();
            }

		    using (var unitOfWork = database.CreateUnitOfWork())
            {
                unitOfWork.DeleteById<SectionNodeProviderDraft>(sectionNodeProviderDraft.SisoId);
                unitOfWork.Commit();
            }
		}

		public void Handle(SectionNameSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x => x.SectionId == domainEvent.AggregateRootId.ToString()).First();
                section.Name = domainEvent.Name;
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
		}

		private SectionNodeProviderDraft GetSectionNodeProviderDraftFromDomainEvent(DomainEvent domainEvent)
		{
            using (var queryEngine = database.CreateQueryEngine())
            {
                return queryEngine.Where<SectionNodeProviderDraft>(x => x.SectionId == domainEvent.AggregateRootId.ToString()).First();
            }
		}

		public void Handle(SectionSequenceSetEvent domainEvent)
		{
            var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x => x.SectionId == sectionNodeProviderDraft.SectionId).First();
                section.Sequence = domainEvent.SectionSequence;
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
		}

		public void Handle(SectionUrlSegmentSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x => x.SectionId == sectionNodeProviderDraft.SectionId).First();
                section.UrlSegment = domainEvent.UrlSegment;
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
		}

        public void Handle(SectionDefaultTreeNodeIdSetEvent domainEvent)
        {
            var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x=> x.SectionId == sectionNodeProviderDraft.SectionId).First();
                section.DefaultTreeNodeId = domainEvent.DefaultTreeNodeId.ToString();
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
        }
        
		public void Handle(SectionTreeNodeIdSetEvent domainEvent)
		{
			var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);
			
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x => x.SectionId == sectionNodeProviderDraft.SectionId).First();
                section.DefaultTreeNodeId = domainEvent.TreeNodeId.ToString();
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
		}

        public void Handle(SectionInactiveSetEvent domainEvent)
        {
            var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x => x.SectionId == sectionNodeProviderDraft.SectionId).First();
                section.Inactive = domainEvent.Inactive;
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
        }

        public void Handle(SectionHiddenSetEvent domainEvent)
        {
            var sectionNodeProviderDraft = GetSectionNodeProviderDraftFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                var section = unitOfWork.Where<SectionNodeProviderDraft>(x => x.SectionId == sectionNodeProviderDraft.SectionId).First();
                section.Hidden = domainEvent.Hidden;
                unitOfWork.Update(section);
                unitOfWork.Commit();
            }
        }
	}
}
