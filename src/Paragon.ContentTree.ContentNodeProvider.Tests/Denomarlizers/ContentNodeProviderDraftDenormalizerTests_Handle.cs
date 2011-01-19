using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Denormalizers;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Domain.Events.Page;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Denomarlizers
{
	[TestClass]
	public class ContentNodeProviderDraftDenormalizerTests_Handle
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Create_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_set_for_PageCreatedEvent()
		{
			var guid = new Guid();
			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageCreatedEvent()
			                                                              	{
			                                                              		AggregateRootId = guid
			                                                              	});

			mocker.GetMock<IContentNodeProviderDraftRepository>().Verify(a => a.Create(It.Is<ContentNodeProviderDraft>(b => b.TreeNodeId == guid.ToString())), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_Name_set_for_PageNameSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageNameSetEvent()
			{
				AggregateRootId = guid,
				Name = "Name",
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Name == "Name" && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_Action_set_for_PageActionSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageActionSetEvent()
			{
				AggregateRootId = guid,
				Action = "Action",
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Action == "Action" && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_MetaTitle_set_for_PageMetaTitleSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new MetaTitleSetEvent()
			{
				AggregateRootId = guid,
				MetaTitle = "MetaTitle"
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.MetaTitle == "MetaTitle" && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_MetaDescription_set_for_PageMetaDescriptionSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new MetaDescriptionSetEvent()
			{
				AggregateRootId = guid,
				MetaDescription = "MetaDescription"
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.MetaDescription == "MetaDescription" && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_UrlSegment_set_for_PageUrlSegmentSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageUrlSegmentSetEvent()
			{
				AggregateRootId = guid,
				UrlSegment = "UrlSegment"
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.UrlSegment == "UrlSegment" && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_HeaderText_set_for_PageHeaderTextSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new HeaderTextSetEvent()
			{
				AggregateRootId = guid,
				HeaderText = "HeaderText"
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.HeaderText == "HeaderText" && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_and_Sequence_set_for_PageSequenceSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				CreateBy = "CreateBy"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageSequenceSetEvent()
			{
				AggregateRootId = guid,
				PageSequence = -1
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Sequence == -1 && b.TreeNodeId == guid.ToString() && b.CreateBy == "CreateBy")), Times.Once());
		}
	}
}
