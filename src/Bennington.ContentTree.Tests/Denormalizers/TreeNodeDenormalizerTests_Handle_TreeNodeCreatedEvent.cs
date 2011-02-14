using System;
using AutoMoq;
using Bennington.ContentTree.Data;
using Bennington.ContentTree.Denormalizers;
using Bennington.ContentTree.Domain.Events.TreeNode;
using Bennington.ContentTree.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Tests.Denormalizers
{
	[TestClass]
	public class TreeNodeDenormalizerTests_Handle_TreeNodeCreatedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sets_Type_of_correct_tree_node()
		{
			var guid = new Guid();
			
			mocker.Resolve<TreeNodeDenormalizer>().Handle(new TreeNodeCreatedEvent()
			                                              	{
			                                              		AggregateRootId = guid,
			                                              	});

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Create(It.Is<TreeNode>(b => b.Id == guid.ToString())), Times.Once());
		}
	}
}
