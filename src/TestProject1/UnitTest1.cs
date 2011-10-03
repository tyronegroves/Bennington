using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Init()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Calls_Update_method_of_IContentNodeProviderPublishedVersionRepository_when_a_matching_published_version_exists()
        {
            mocker = null;
            mocker = new AutoMoqer();
            var pageId = "49d114b4-d992-48a7-a7fb-b7546cf5bcdb";
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
                AggregateRootId = new Guid(pageId),
            });

            mocker.GetMock<IContentNodeProviderPublishedVersionRepository>().Verify(a => a.Update(It.IsAny<ContentNodeProviderPublishedVersion>()), Times.Once());
        }
    }
}
