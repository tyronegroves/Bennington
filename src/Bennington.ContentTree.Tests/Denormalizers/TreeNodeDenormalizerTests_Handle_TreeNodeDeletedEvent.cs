using System;
using AutoMoq;
using Bennington.ContentTree.Denormalizers;
using Bennington.ContentTree.Domain.Events.TreeNode;
using Bennington.ContentTree.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Tests.Denormalizers
{
	[TestClass]
	public class TreeNodeDenormalizerTests_Handle_TreeNodeDeletedEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_delete_method_of_ITreeNodeRepos()
		{
			var guid = new Guid();

			mocker.Resolve<TreeNodeDenormalizer>().Handle(new TreeNodeDeletedEvent()
			                                              	{
			                                              		AggregateRootId = guid,
			                                              	});

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Delete(guid.ToString()), Times.Once());
		}
	}
}
