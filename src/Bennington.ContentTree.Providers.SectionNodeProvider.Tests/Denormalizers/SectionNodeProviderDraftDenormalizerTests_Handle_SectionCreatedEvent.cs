using System;
using AutoMoq;
using Bennington.ContentTree.Domain.Events.Section;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using Bennington.ContentTree.Providers.SectionNodeProvider.Denormalizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Tests.Denormalizers
{
	[TestClass]
	public class SectionNodeProviderDraftDenormalizerTests_Handle_SectionCreatedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Create_ethod_of_IDataModelDataContext_with_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			
			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionCreatedEvent()
			                                                              	{
			                                                              		AggregateRootId = id,
																				SectionId = id
			                                                              	});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Create(It.Is<SectionNodeProviderDraft>(b => b.SectionId == id.ToString())), Times.Once());
		}
	}
}
