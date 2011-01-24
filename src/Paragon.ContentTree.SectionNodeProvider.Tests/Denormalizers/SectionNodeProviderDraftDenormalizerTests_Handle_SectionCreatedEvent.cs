using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Domain.Events.Section;
using Paragon.ContentTree.SectionNodeProvider.Data;
using Paragon.ContentTree.SectionNodeProvider.Denormalizers;

namespace Paragon.ContentTree.SectionNodeProvider.Tests.Denormalizers
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
			                                                              		AggregateRootId = id
			                                                              	});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Create(It.Is<SectionNodeProviderDraft>(b => b.SectionId == id.ToString())), Times.Once());
		}
	}
}
