using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.Tests.Contexts
{
	[TestClass]
	public class TreeNodeSummaryContextTests_Create
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void INit()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_newly_created_tree_node()
		{
			var treeNode = new TreeNodeSummary()
			               	{
			               		Id = "id",
			               	};
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.Create(It.IsAny<TreeNode>()))
				.Returns(new TreeNode()
				         	{
				         		Id = "id",
				         	});

			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummaryContext.Create("parentNodeId", typeof(FakeTreeNodeExtensionProvider));

			Assert.AreEqual(treeNode.Id, result);
		}

		[TestMethod]
		public void Calls_Create_method_of_ITreeNodeRepository_with_new_node_with_correct_ParentTreeNodeId_property()
		{
			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();
			treeNodeSummaryContext.Create("parentNodeId", typeof(FakeTreeNodeExtensionProvider));

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Create(It.Is<TreeNode>(b => b.ParentTreeNodeId == "parentNodeId")));
		}

		[TestMethod]
		public void Calls_Create_method_of_ITreeNodeRepository_with_new_node_with_correct_Type_property()
		{
			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();
			treeNodeSummaryContext.Create("parentNodeId", typeof (FakeTreeNodeExtensionProvider));

			mocker.GetMock<ITreeNodeRepository>().Verify(a => a.Create(It.Is<TreeNode>(b => b.Type == typeof(FakeTreeNodeExtensionProvider).FullName)));
		}

		[TestMethod]
		public void Does_not_throw_exception_when_passing_type_that_does_implements_IAmATreeNodeExtensionProvider()
		{
			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();
			treeNodeSummaryContext.Create(null, typeof(FakeTreeNodeExtensionProvider));
		}

		[TestMethod]
		public void Throws_exception_when_passing_type_that_does_not_implement_IAmATreeNodeExtensionProvider()
		{
			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();

			try
			{
				treeNodeSummaryContext.Create(null, typeof(string));
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.Message.StartsWith("Provider type must implement "));
				return;
			}
			throw new Exception("Should throw excpetion above");
		}

		private class FakeTreeNodeExtensionProvider : IAmATreeNodeExtensionProvider
		{
			public IQueryable<IAmATreeNodeExtension> GetAll()
			{
				throw new NotImplementedException();
			}

			public string Name
			{
				get { throw new NotImplementedException(); }
			}

			public string ControllerToUseForProcessing
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public string ControllerToUseForModification
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public string ActionToUseForModification
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public string ControllerToUseForCreation
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public string ActionToUseForCreation
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public IRouteConstraint IgnoreConstraint
			{
				get { throw new NotImplementedException(); }
			}

			public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}
		}
	}
}
