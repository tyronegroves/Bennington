using System;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Denomarlizers
{
	[TestClass]
	public class ContentNodeProviderPublishDenormalizerTests_Handle_PagePublishedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Create_method_of_IContentNodeProviderPublishedVersionRepository_when_a_matching_published_version_does_not_exist()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
					.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			                                                                	{
			                                                                		AggregateRootId = pageId,
			                                                                	});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Verify(a => a.Create(It.IsAny<ContentNodeProviderPublishedVersion>()), Times.Once());
		}

		[TestMethod]
		public void Calls_Create_method_of_IContentNodeProviderPublishedVersionRepository_with_mapper_result_when_a_matching_published_version_does_not_exist()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>()
				.Setup(a => a.CreateInstance(It.IsAny<ContentNodeProviderDraft>()))
				.Returns(new ContentNodeProviderPublishedVersion()
							{
								Name = "x",
							});
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x"
				         			}, 
							}.AsQueryable());


			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Verify(a => a.Create(It.Is<ContentNodeProviderPublishedVersion>(b => b.Name == "x")), Times.Once());
		}

		[TestMethod]
		public void Passess_existing_draft_version_to_mapper_when_existing_published_version_does_not_exist()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>()
				.Verify(a => a.CreateInstance(It.Is<ContentNodeProviderDraft>(b => b.Name == "x" && b.PageId == pageId.ToString())), Times.Once());
		}

		[TestMethod]
		public void Does_not_call_Create_method_of_IContentNodeProviderPublishedVersionRepository_when_a_matching_published_version_exists()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
										PageId = pageId.ToString(),
				         				Body = "body",
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Verify(a => a.Create(It.IsAny<ContentNodeProviderPublishedVersion>()), Times.Never());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderPublishedVersionRepository_when_a_matching_published_version_exists()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x"
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
										PageId = pageId.ToString(),
				         				Body = "body",
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Verify(a => a.Update(It.IsAny<ContentNodeProviderPublishedVersion>()), Times.Once());
		}

		[TestMethod]
		public void Calls_Update_method_of_IContentNodeProviderPublishedVersionRepository_with_result_of_mapper_when_a_matching_published_version_exists()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>()
				.Setup(a => a.CreateInstance(It.IsAny<ContentNodeProviderDraft>()))
				.Returns(new ContentNodeProviderPublishedVersion()
				         	{
				         		PageId = pageId.ToString(),
								Name = "name",
				         	});
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x"
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
										PageId = pageId.ToString(),
				         				Body = "body",
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Verify(a => a.Update(It.Is<ContentNodeProviderPublishedVersion>(b => b.Name == "name")), Times.Once());
		}

		[TestMethod]
		public void Passess_existing_draft_version_to_mapper_when_existing_published_version_exists()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x"
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
										PageId = pageId.ToString(),
				         				Body = "body",
				         			}, 
							}.AsQueryable());


			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>()
				.Verify(a => a.CreateInstance(It.Is<ContentNodeProviderDraft>(b => b.Name == "x" && b.PageId == pageId.ToString())), Times.Once());
		}

		[TestMethod]
		public void Copies_header_image_file_from_draft_mode_folder()
		{
			var publishedVersionFileUploadPath = @"c:\publishedVersionUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.PublishedVersionFileUploadPath"))
				.Returns(publishedVersionFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x",
										HeaderImage = "test.jpg",
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IFileSystem>().Setup(a => a.FileExists(draftFileUploadPath + pageId + @"\HeaderImage\test.jpg")).Returns(true);


			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
																					{
																						AggregateRootId = pageId,
																					});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.Copy(draftFileUploadPath + pageId + @"\HeaderImage\test.jpg", publishedVersionFileUploadPath + pageId + @"\HeaderImage\test.jpg"), Times.Once());
		}

		[TestMethod]
		public void Does_not_attempt_to_copy_header_image_file_from_draft_mode_folder_if_there_is_no_image_there()
		{
			var publishedVersionFileUploadPath = @"c:\publishedVersionUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.PublishedVersionFileUploadPath"))
				.Returns(publishedVersionFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x",
										HeaderImage = "test.jpg",
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IFileSystem>().Setup(a => a.FileExists(draftFileUploadPath + pageId + @"\HeaderImage\test.jpg"))
				.Returns(false);

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
																				{
																					AggregateRootId = pageId,
																				});

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.Copy(draftFileUploadPath + pageId + @"\HeaderImage\test.jpg", publishedVersionFileUploadPath + pageId + @"\HeaderImage\test.jpg"), Times.Never());
		}

		[TestMethod]
		public void Creates_published_version_header_image_folder_if_it_does_not_exist()
		{
			var pageId = Guid.NewGuid();
			var publishedVersionFileUploadPath = @"c:\publishedVersionUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.PublishedVersionFileUploadPath"))
				.Returns(publishedVersionFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
							}.AsQueryable());
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x",
										HeaderImage = "test.jpg",
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
																					{
																						AggregateRootId = pageId,
																					});

			mocker.GetMock<IFileSystem>().Verify(a => a.CreateFolder(publishedVersionFileUploadPath + pageId + @"\HeaderImage"), Times.Once());
		}

		[TestMethod]
		public void Does_not_attempt_to_create_published_version_header_image_folder_if_it_exists()
		{
			var pageId = Guid.NewGuid();
			var publishedVersionFileUploadPath = @"c:\publishedVersionUpload\";
			var draftFileUploadPath = @"c:\draftUpload\";
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.PublishedVersionFileUploadPath"))
				.Returns(publishedVersionFileUploadPath);
			mocker.GetMock<IApplicationSettingsValueGetter>()
				.Setup(a => a.GetValue("Bennington.ContentTree.Providers.ContentNodeProvider.DraftFileUploadPath"))
				.Returns(draftFileUploadPath);
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
							}.AsQueryable());
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				PageId = pageId.ToString(),
										Name = "x",
										HeaderImage = "test.jpg",
				         			}, 
							}.AsQueryable());
			mocker.GetMock<IFileSystem>().Setup(a => a.DirectoryExists(publishedVersionFileUploadPath + pageId + @"\HeaderImage"))
				.Returns(true);

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IFileSystem>().Verify(a => a.CreateFolder(publishedVersionFileUploadPath + pageId + @"\HeaderImage"), Times.Never());
		}

		[TestMethod]
		public void Does_not_throw_if_the_draft_version_does_not_exist()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PagePublishedEvent()
																					{
																						AggregateRootId = pageId,
																					});
		}
	}
}
