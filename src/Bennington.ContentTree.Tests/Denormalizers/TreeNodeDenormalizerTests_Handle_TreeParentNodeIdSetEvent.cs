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
	public class TreeNodeDenormalizerTests_Handle_TreeNodeParentNodeIdSetEvent
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sets_ParentNodeId_of_correct_tree_node()
		{
			var guid = new Guid();
			var parentTreeNodeId = new Guid();
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
				         				Id = guid.ToString(),
										Type = "test",
				         			}, 
							}.AsQueryable());

			mocker.Resolve<TreeNodeDenormalizer>().Handle(new TreeNodeParentTreeNodeIdSetEvent()
			                                              	{
			                                              		AggregateRootId = guid,
																ParentTreeNodeId = parentTreeNodeId
			                                              	});

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Update(It.Is<TreeNode>(b => b.Type == "test" && b.ParentTreeNodeId == parentTreeNodeId.ToString() && b.Id == guid.ToString())), Times.Once());
		}
	}
}
