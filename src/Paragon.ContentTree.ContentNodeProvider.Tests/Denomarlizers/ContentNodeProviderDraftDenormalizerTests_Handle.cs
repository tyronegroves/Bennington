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
		public void Calls_Create_method_of_IContentNodeProviderDraftRepository_with_TreeNodeId_set()
		{
			var guid = new Guid();
			mocker.Resolve<ContentNodeProviderDraftDenormalizer>().Handle(new PageCreatedEvent()
			                                                              	{
			                                                              		AggregateRootId = guid
			                                                              	});

			mocker.GetMock<IContentNodeProviderDraftRepository>().Verify(a => a.Create(It.Is<ContentNodeProviderDraft>(b => b.TreeNodeId == guid.ToString())), Times.Once());
		}
	}
}
