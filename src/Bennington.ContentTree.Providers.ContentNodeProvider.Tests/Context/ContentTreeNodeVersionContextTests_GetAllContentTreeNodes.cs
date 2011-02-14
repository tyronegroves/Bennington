using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Providers.ContentNodeProvider.Context;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Context
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
