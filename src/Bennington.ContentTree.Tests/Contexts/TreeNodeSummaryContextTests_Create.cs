using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Data;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Models;
using Bennington.ContentTree.Repositories;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Tests.Contexts
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
			var guid = new Guid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid()).Returns(guid);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.Create(It.IsAny<TreeNode>()))
				.Returns(new TreeNode()
				         	{
				         		Id = "id",
				         	});

			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummaryContext.Create("parentNodeId", typeof(FakeTreeNodeExtensionProvider).AssemblyQualifiedName);

			Assert.AreEqual(guid.ToString(), result);
		}

		[TestMethod]
		public void Does_not_throw_exception_when_passing_type_that_does_implements_IAmATreeNodeExtensionProvider()
		{
			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();
			treeNodeSummaryContext.Create(null, typeof(FakeTreeNodeExtensionProvider).AssemblyQualifiedName);
		}

		[TestMethod]
		public void Throws_exception_when_passing_type_that_does_not_implement_IAmATreeNodeExtensionProvider()
		{
			var treeNodeSummaryContext = mocker.Resolve<TreeNodeSummaryContext>();

			try
			{
				treeNodeSummaryContext.Create(null, typeof(string).AssemblyQualifiedName);
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.Message.StartsWith("Provider type must implement "));
				return;
			}
			throw new Exception("Should throw excpetion above");
		}

		public class FakeProvider : IAmATreeNodeExtensionProvider
		{
			public IQueryable<IAmATreeNodeExtension> GetAll()
			{
				throw new NotImplementedException();
			}

			public string Name
			{
				get { throw new NotImplementedException(); }
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

			public bool MayHaveChildNodes
			{
				get { return false; }
				set { throw new NotImplementedException(); }
			}

			public void RegisterRouteForTreeNodeId(string treeNodeId)
			{
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void Create_method_sends_CreateTreeNodeCommand()
		{
			var guid = new Guid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid()).Returns(guid);

			mocker.Resolve<TreeNodeSummaryContext>().Create("parentNodeId", typeof(FakeProvider).AssemblyQualifiedName);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateTreeNodeCommand>(b => b.ParentId == "parentNodeId"
																									&& b.TreeNodeId == guid
																									&& b.Type == typeof(FakeProvider)
																									&& b.AggregateRootId == guid)), Times.Once());
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

			public bool MayHaveChildNodes
			{
				get { return false; }
				set { throw new NotImplementedException(); }
			}

			public void RegisterRouteForTreeNodeId(string treeNodeId)
			{
				throw new NotImplementedException();
			}
		}
	}
}
