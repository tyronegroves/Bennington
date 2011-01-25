using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Denormalizers;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Domain.Events.Page;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Denomarlizers
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

	}
}
