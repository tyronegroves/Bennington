using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.DeNormalizers;
using Paragon.ContentTree.Domain.Events.Page;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.Tests.Denormalizers
{
	[TestClass]
	public class TreeNodeDenormalizerTests_Handle_PageCreatedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Creates_new_tree_node_with_correct_id()
		{
			var guid = new Guid();

			mocker.Resolve<TreeNodeDenormalizer>().Handle(new PageCreatedEvent()
			                                              	{
			                                              		AggregateRootId = guid,
																ProviderType = typeof(ContentNodeProvider.ContentNodeProvider),
			                                              	});

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Create(It.Is<TreeNode>(b => b.Id == guid.ToString())), Times.Once());
		}
	}
}
