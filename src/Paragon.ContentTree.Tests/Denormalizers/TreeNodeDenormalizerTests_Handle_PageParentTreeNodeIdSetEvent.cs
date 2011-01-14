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
	public class TreeNodeDenormalizerTests_Handle_PageParentTreeNodeIdSetEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sets_ParentTreeNodeId()
		{
			var treeNodeId = new Guid();
			var parentTreeNodeId = new Guid();
			var treeNode = new TreeNode()
							{
								Id = treeNodeId.ToString(),
								CreateBy = "desired TreeNode",
								ParentTreeNodeId = parentTreeNodeId.ToString()
							};
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[] { treeNode }.AsQueryable());

			mocker.Resolve<TreeNodeDenormalizer>().Handle(new PageParentTreeNodeIdSetEvent()
			                                              	{
			                                              		ParentTreeNodeId = parentTreeNodeId,
																AggregateRootId = treeNodeId,
			                                              	});

			mocker.GetMock<ITreeNodeRepository>().Verify(a =>a.Update(It.Is<TreeNode>(b => b.CreateBy == treeNode.CreateBy)), Times.Once());
		}
	}
}
