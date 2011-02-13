using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Contexts;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Context
{
	[TestClass]
	public class ContentTreeNodeVersionContextTests_GetAllContentTreeNodes
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_results_from_IContentNodeProviderDraftToContentTreeNodeMapper_when_IVersionContext_returns_Draft()
		{
			mocker.GetMock<IVersionContext>().Setup(a => a.GetCurrentVersionId()).Returns(VersionContext.Draft);
			mocker.GetMock<IContentNodeProviderDraftToContentTreeNodeMapper>().Setup(a => a.CreateSet(It.IsAny<IEnumerable<ContentNodeProviderDraft>>()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				Action = "Action",
				         			}, 
							});

			var result = mocker.Resolve<ContentTreeNodeVersionContext>().GetAllContentTreeNodes();

			Assert.AreEqual("Action", result.First().Action);
		}

		[TestMethod]
		public void Passes_drafts_from_IContentNodeProviderDraftRepository_to_IContentNodeProviderDraftToContentTreeNodeMapper_when_IVersionContext_returns_Draft()
		{
			mocker.GetMock<IVersionContext>().Setup(a => a.GetCurrentVersionId()).Returns(VersionContext.Draft);
			mocker.GetMock<IContentNodeProviderDraftRepository>().Setup(a => a.GetAllContentNodeProviderDrafts())
				.Returns(new ContentNodeProviderDraft[]
				         	{
				         		new ContentNodeProviderDraft()
				         			{
				         				Action = "Action"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentTreeNodeVersionContext>().GetAllContentTreeNodes();

			mocker.GetMock<IContentNodeProviderDraftToContentTreeNodeMapper>()
				.Verify(a => a.CreateSet(It.Is<IEnumerable<ContentNodeProviderDraft>>(b => b.First().Action == "Action")), Times.Once());
		}

		[TestMethod]
		public void Returns_results_from_IContentNodeProviderPublishedVersionToContentTreeNodeMapper_when_IVersionContext_returns_Publish()
		{
			mocker.GetMock<IVersionContext>().Setup(a => a.GetCurrentVersionId()).Returns(VersionContext.Publish);
			mocker.GetMock<IContentNodeProviderPublishedVersionToContentTreeNodeMapper>().Setup(a => a.CreateSet(It.IsAny<IEnumerable<ContentNodeProviderPublishedVersion>>()))
				.Returns(new ContentTreeNode[]
			                {
			                    new ContentTreeNode()
			                        {
			                            Action = "Action",
			                        }, 
			                });

			var result = mocker.Resolve<ContentTreeNodeVersionContext>().GetAllContentTreeNodes();

			Assert.AreEqual("Action", result.First().Action);
		}

		[TestMethod]
		public void Passes_published_versions_from_IContentNodeProviderPublishedVersionRepository_to_IContentNodeProviderPublishedVersionToContentTreeNodeMapper_when_IVersionContext_returns_Publish()
		{
			mocker.GetMock<IVersionContext>().Setup(a => a.GetCurrentVersionId()).Returns(VersionContext.Publish);
			mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Setup(a => a.GetAllContentNodeProviderPublishedVersions())
				.Returns(new ContentNodeProviderPublishedVersion[]
				         	{
				         		new ContentNodeProviderPublishedVersion()
				         			{
				         				Action = "Action"
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ContentTreeNodeVersionContext>().GetAllContentTreeNodes();

			mocker.GetMock<IContentNodeProviderPublishedVersionToContentTreeNodeMapper>()
				.Verify(a => a.CreateSet(It.Is<IEnumerable<ContentNodeProviderPublishedVersion>>(b => b.First().Action == "Action")), Times.Once());
		}
	}
}
