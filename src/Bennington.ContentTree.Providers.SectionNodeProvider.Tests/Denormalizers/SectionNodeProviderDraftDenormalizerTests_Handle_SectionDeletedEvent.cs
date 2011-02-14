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
	public class SectionNodeProviderDraftDenormalizerTests_Handle_SectionDeletedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Delete_method_of_IDataModelDataContext_with_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			mocker.GetMock<IDataModelDataContext>().Setup(a => a.GetAllSectionNodeProviderDrafts())
				.Returns(new SectionNodeProviderDraft[]
				         	{
				         		new SectionNodeProviderDraft()
				         			{
				         				Name = "x",
										TreeNodeId = id.ToString(),
				         			}, 
						});
			
			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionDeletedEvent()
			                                                              	{
			                                                              		AggregateRootId = id
			                                                              	});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Delete(It.Is<SectionNodeProviderDraft>(b => b.Name =="x" && b.TreeNodeId == id.ToString())), Times.Once());
		}
	}
}
