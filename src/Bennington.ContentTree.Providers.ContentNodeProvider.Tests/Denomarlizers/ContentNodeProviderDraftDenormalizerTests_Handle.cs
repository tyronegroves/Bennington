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

			mocker.GetMock<IContentNodeProviderDraftRepository>().Verify(a => a.Create(It.Is<ContentNodeProviderDraft>(b => b.PageId == guid.ToString())), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_Name_set_for_PageNameSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageNameSetEvent()
																				{
																					AggregateRootId = guid,
																					Name = "Name",
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Name == "Name" && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_Action_set_for_PageActionSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageActionSetEvent()
																				{
																					AggregateRootId = guid,
																					Action = "Action",
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Action == "Action" && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_MetaTitle_set_for_PageMetaTitleSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new MetaTitleSetEvent()
																				{
																					AggregateRootId = guid,
																					MetaTitle = "MetaTitle"
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.MetaTitle == "MetaTitle" && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_MetaDescription_set_for_PageMetaDescriptionSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new MetaDescriptionSetEvent()
																				{
																					AggregateRootId = guid,
																					MetaDescription = "MetaDescription"
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.MetaDescription == "MetaDescription" && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_UrlSegment_set_for_PageUrlSegmentSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageUrlSegmentSetEvent()
																				{
																					AggregateRootId = guid,
																					UrlSegment = "UrlSegment"
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.UrlSegment == "UrlSegment" && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_HeaderText_set_for_PageHeaderTextSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new HeaderTextSetEvent()
																				{
																					AggregateRootId = guid,
																					HeaderText = "HeaderText"
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.HeaderText == "HeaderText" && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_Sequence_set_for_PageSequenceSetEvent()
		{
			var guid = new Guid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageSequenceSetEvent()
																				{
																					AggregateRootId = guid,
																					PageSequence = -1
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Sequence == -1 && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_TreeNodeId_set_for_PageTreeNodeIdSetEvent()
		{
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = pageId.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageTreeNodeIdSetEvent()
																				{
																					AggregateRootId = pageId,
																					TreeNodeId = treeNodeId,
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.TreeNodeId == treeNodeId.ToString() && b.PageId == pageId.ToString() && b.Body == "Body")), Times.Once());
		}


		[TestMethod]
		public void Calls_Delete_method_of_IContentNodeProviderDraftRepository_with_every_instance_of_ContentProviderDraft_matching_by_TreeNodeId_when_handling_PageDeletedEvent()
		{
			var treeNodeId = Guid.NewGuid();
			var guid1 = Guid.NewGuid();
			var guid2 = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = guid1.ToString(),
										TreeNodeId = treeNodeId.ToString(),
				         				Body = "Body"
				         			}, 
								new ContentNodeProviderDraft()
				         			{
										PageId = guid2.ToString(),
										TreeNodeId = treeNodeId.ToString(),
				         				Body = "Body2"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageDeletedEvent()
																				{
																					TreeNodeId = treeNodeId
																				});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Delete(It.Is<ContentNodeProviderDraft>(b => b.TreeNodeId == treeNodeId.ToString() && b.PageId == guid1.ToString())), Times.Once());
			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Delete(It.Is<ContentNodeProviderDraft>(b => b.TreeNodeId == treeNodeId.ToString() && b.PageId == guid2.ToString())), Times.Once());
		}

		[TestMethod]
		public void Does_not_call_repository_delete_method_when_handling_delete_event_for_a_page_that_does_not_exist()
		{
			var guid = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = guid.ToString(),
				         				Body = "Body"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageDeletedEvent()
			{
				TreeNodeId = new Guid(),
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Delete(It.IsAny<ContentNodeProviderDraft>()), Times.Never());
		}
	}
}
