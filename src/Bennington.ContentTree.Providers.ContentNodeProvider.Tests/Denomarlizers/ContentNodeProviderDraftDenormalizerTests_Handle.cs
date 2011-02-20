using System;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Denomarlizers
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
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_Hidden_set_for_PageHiddenSetEvent()
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

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHiddenSetEvent()
			{
				AggregateRootId = guid,
				Hidden = true,
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Hidden && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_PageId_and_Inactive_set_for_PageHiddenSetEvent()
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

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageInactiveSetEvent()
			{
				AggregateRootId = guid,
				Inactive = true
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.Inactive && b.PageId == guid.ToString() && b.Body == "Body")), Times.Once());
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
		public void Calls_Update_method_of_IContentNodeProviderDraftRepository_with_HeaderImage_and_TreeNodeId_set_for_PageTreeNodeIdSetEvent()
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

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
			{
				AggregateRootId = pageId,
				HeaderImage = "test"
			});

			mocker.GetMock<IContentNodeProviderDraftRepository>()
				.Verify(a => a.Update(It.Is<ContentNodeProviderDraft>(b => b.HeaderImage == "test" && b.PageId == pageId.ToString() && b.Body == "Body")), Times.Once());
		}

		[TestMethod]
		public void Copies_header_image_file_from_provider_upload_folder_to_draft_mode_upload_folder_location()
		{
			var providerFileUploadPath = @"c:\providerUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath"))
				.Returns(providerFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = treeNodeId.ToString(),
										PageId = pageId.ToString(),
				         				Body = "Body",
										Action = "Test"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
																				{
																					AggregateRootId = pageId,
																					HeaderImage = "test.jpg"
																				});
			
			mocker.GetMock<IFileSystem>()
				.Verify(a => a.Copy(providerFileUploadPath + treeNodeId.ToString() + @"\Test\HeaderImage\test.jpg", 
									draftFileUploadPath + pageId + @"\HeaderImage\test.jpg"), Times.Once());
		}

		[TestMethod]
		public void Does_not_copy_header_image_file_from_provider_upload_folder_to_draft_mode_upload_folder_location_when_the_HeaderImage_property_is_null()
		{
			var providerFileUploadPath = @"c:\providerUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath"))
				.Returns(providerFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = treeNodeId.ToString(),
										PageId = pageId.ToString(),
				         				Body = "Body",
										Action = "Test"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.Copy(providerFileUploadPath + treeNodeId.ToString() + @"\Test\HeaderImage\",
									It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void Does_not_copy_header_image_file_from_provider_upload_folder_to_draft_mode_upload_folder_location_when_the_HeaderImage_property_is_empty()
		{
			var providerFileUploadPath = @"c:\providerUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath"))
				.Returns(providerFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										TreeNodeId = treeNodeId.ToString(),
										PageId = pageId.ToString(),
				         				Body = "Body",
										Action = "Test"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
			{
				AggregateRootId = pageId,
				HeaderImage = string.Empty
			});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.Copy(providerFileUploadPath + treeNodeId.ToString() + @"\Test\HeaderImage\",
									It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void Attempts_to_delete_existing_header_image()
		{
			var providerFileUploadPath = @"c:\providerUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath"))
				.Returns(providerFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = pageId.ToString(),
				         				Body = "Body",
										HeaderImage = "old header image.jpg"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
			{
				AggregateRootId = pageId,
				HeaderImage = "new header image.jpg"
			});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.DeleteFile(draftFileUploadPath + pageId + @"\HeaderImage\old header image.jpg"), Times.Once());
		}

		[TestMethod]
		public void Attempts_to_create_draft_mode_file_upload_folder_if_it_doesnt_exist()
		{
			var providerFileUploadPath = @"c:\providerUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath"))
				.Returns(providerFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = pageId.ToString(),
				         				Body = "Body",
										HeaderImage = "old header image.jpg"
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.DirectoryExists(draftFileUploadPath + pageId + @"\HeaderImage"))
				.Returns(false);

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
			{
				AggregateRootId = pageId,
				HeaderImage = "new header image.jpg"
			});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.CreateFolder(draftFileUploadPath + pageId + @"\HeaderImage"), Times.Once());
		}

		[TestMethod]
		public void Does_not_attempt_to_create_draft_mode_file_upload_folder_if_it_already_exists()
		{
			var providerFileUploadPath = @"c:\providerUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.FileUploadPath"))
				.Returns(providerFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
										PageId = pageId.ToString(),
				         				Body = "Body",
										HeaderImage = "old header image.jpg"
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.DirectoryExists(draftFileUploadPath + pageId + @"\HeaderImage"))
				.Returns(true);

			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageHeaderImageSetEvent()
			{
				AggregateRootId = pageId,
				HeaderImage = "new header image.jpg"
			});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.CreateFolder(draftFileUploadPath + pageId + @"\HeaderImage"), Times.Never());
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
