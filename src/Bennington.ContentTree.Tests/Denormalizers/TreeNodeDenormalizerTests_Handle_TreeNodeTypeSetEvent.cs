using System;
using System.Linq;
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
	public class TreeNodeDenormalizerTests_Handle_TreeNodeTypeSetEvent
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
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
				         				Id = guid.ToString(),
										ParentTreeNodeId = "test",
				         			}, 
							}.ToList());

			mocker.Resolve<TreeNodeDenormalizer>().Handle(new TreeNodeTypeSetEvent()
			                                              	{
			                                              		AggregateRootId = guid,
																Type = typeof(string)
			                                              	});

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Update(It.Is<TreeNode>(b => b.ParentTreeNodeId == "test" && b.Type == typeof(string).AssemblyQualifiedName && b.Id == guid.ToString())), Times.Once());
		}
	}
}
