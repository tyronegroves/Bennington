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
	public class ContentNodeProviderPublishDenormalizerTests_Handle_PageDeletedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Verify_that_Delete_method_of_IContentNodeProviderPublishedVersionRepository_is_called_with_matching_instance_from_repository()
		{
			//var pageId = Guid.NewGuid();
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
										TreeNodeId = treeNodeId.ToString(),
				         				//PageId = pageId.ToString(),
										Name = "name"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PageDeletedEvent()
			                                                                	{
			                                                                		TreeNodeId = treeNodeId
			                                                                	});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>()
				.Verify(a => a.Delete(It.Is<ContentNodeProviderPublishedVersion>(b => b.Name == "name")), Times.Once());
		}

		[TestMethod]
		public void Does_call_Delete_when_attempting_to_delete_a_published_version_that_no_longer_exists()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]{}.AsQueryable());

			mocker.Resolve<ContentNodeProviderPublishDenormalizer>().Handle(new PageDeletedEvent()
			{
				AggregateRootId = pageId,
			});

			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>()
				.Verify(a => a.Delete(It.IsAny<ContentNodeProviderPublishedVersion>()), Times.Never());
		}
	}
}
