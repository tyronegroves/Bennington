using System;
using System.Data.Linq;
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
	public class SectionNodeProviderDraftDenormalizerTests_Handle
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Update_method_of_IDataModelDataContext_with_correct_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			mocker.GetMock<IDataModelDataContext>().Setup(a => a.GetAllSectionNodeProviderDrafts())
				.Returns(new SectionNodeProviderDraft[]
								{
									new SectionNodeProviderDraft()
										{
											SectionId = id.ToString(),
											DefaultTreeNodeId = "x",
										}, 
								});
			
			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionNameSetEvent()
			                                                              	{
			                                                              		AggregateRootId = id,
																				Name = "name",
			                                                              	});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Update(It.Is<SectionNodeProviderDraft>(b => 
				b.Name == "name" 
				&& b.DefaultTreeNodeId == "x" 
				&& b.SectionId == id.ToString())), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IDataModelDataContext_with_correct_Sequence_property_value_set_on_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			mocker.GetMock<IDataModelDataContext>().Setup(a => a.GetAllSectionNodeProviderDrafts())
				.Returns(new SectionNodeProviderDraft[]
								{
									new SectionNodeProviderDraft()
										{
											SectionId = id.ToString(),
											DefaultTreeNodeId = "x",
										}, 
								});

			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionSequenceSetEvent()
			{
				AggregateRootId = id,
				SectionSequence = 10,
			});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Update(It.Is<SectionNodeProviderDraft>(b =>
				b.Sequence == 10
				&& b.DefaultTreeNodeId == "x"
				&& b.SectionId == id.ToString())), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IDataModelDataContext_with_correct_UrlSegment_property_value_set_on_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			mocker.GetMock<IDataModelDataContext>().Setup(a => a.GetAllSectionNodeProviderDrafts())
				.Returns(new SectionNodeProviderDraft[]
								{
									new SectionNodeProviderDraft()
										{
											SectionId = id.ToString(),
											DefaultTreeNodeId = "x",
										}, 
								});

			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionUrlSegmentSetEvent()
			{
				AggregateRootId = id,
				UrlSegment = "url"
			});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Update(It.Is<SectionNodeProviderDraft>(b =>
				b.UrlSegment == "url"
				&& b.DefaultTreeNodeId == "x"
				&& b.SectionId == id.ToString())), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IDataModelDataContext_with_correct_DefaultTreeNodeId_property_value_set_on_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			var defaultTreeNodeId = Guid.NewGuid();
			mocker.GetMock<IDataModelDataContext>().Setup(a => a.GetAllSectionNodeProviderDrafts())
				.Returns(new SectionNodeProviderDraft[]
								{
									new SectionNodeProviderDraft()
										{
											SectionId = id.ToString(),
											Name = "x"
										}, 
								});
			
			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionDefaultTreeNodeIdSetEvent()
			{
				AggregateRootId = id,
				DefaultTreeNodeId = defaultTreeNodeId
			});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Update(It.Is<SectionNodeProviderDraft>(b =>
				b.DefaultTreeNodeId == defaultTreeNodeId.ToString()
				&& b.Name == "x"
				&& b.SectionId == id.ToString())), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IDataModelDataContext_with_correct_TreeNodeId_property_value_set_on_SectionNodeProvider_instance()
		{
			var id = Guid.NewGuid();
			var defaultTreeNodeId = Guid.NewGuid();
			mocker.GetMock<IDataModelDataContext>().Setup(a => a.GetAllSectionNodeProviderDrafts())
				.Returns(new SectionNodeProviderDraft[]
								{
									new SectionNodeProviderDraft()
										{
											SectionId = id.ToString(),
											Name = "x"
										}, 
								});

			mocker.Resolve<SectionNodeProviderDraftDenormalizer>().Handle(new SectionTreeNodeIdSetEvent()
			{
				AggregateRootId = id,
				TreeNodeId = defaultTreeNodeId
			});

			mocker.GetMock<IDataModelDataContext>().Verify(a => a.Update(It.Is<SectionNodeProviderDraft>(b =>
				b.TreeNodeId == defaultTreeNodeId.ToString()
				&& b.Name == "x"
				&& b.SectionId == id.ToString())), Times.Once());
		}
	}
}
