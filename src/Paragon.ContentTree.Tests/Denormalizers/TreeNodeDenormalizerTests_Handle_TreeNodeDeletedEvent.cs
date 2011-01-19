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
using Paragon.ContentTree.Domain.Events.TreeNode;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.Tests.Denormalizers
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
