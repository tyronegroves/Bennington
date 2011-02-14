using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Data;
using Bennington.ContentTree.Models;
using Bennington.ContentTree.Repositories;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Tests.Contexts
{
	[TestClass]
	public class TreeNodeSummaryContextTests_GetChildren
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_1_result_with_ControllerToUseForCreation_property_set_from_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			fakeTreeNodeExtensionProvider.SetupProperty(a => a.ControllerToUseForCreation, "FakeTreeNodeExtensionProviderController");
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			}, 
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("FakeTreeNodeExtensionProviderController", result.First().ControllerToUseForCreation);
		}

		[TestMethod]
		public void Returns_1_result_with_ActionToUseForCreation_property_set_from_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			fakeTreeNodeExtensionProvider.SetupProperty(a => a.ActionToUseForCreation, "FakeTreeNodeExtensionProviderAction");
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			},
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			},
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			},
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("FakeTreeNodeExtensionProviderAction", result.First().ActionToUseForCreation);
		}

		[TestMethod]
		public void Returns_1_result_with_ActionToUseForModification_property_set_from_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			fakeTreeNodeExtensionProvider.SetupProperty(a => a.ActionToUseForModification, "FakeTreeNodeExtensionProviderAction");
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			},
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			},
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			},
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("FakeTreeNodeExtensionProviderAction", result.First().ActionToUseForModification);
		}

		[TestMethod]
		public void Returns_1_result_with_ControllerToUseForModification_property_set_from_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			fakeTreeNodeExtensionProvider.SetupProperty(a => a.ControllerToUseForModification, "FakeTreeNodeExtensionProviderController");
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			}, 
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("FakeTreeNodeExtensionProviderController", result.First().ControllerToUseForModification);
		}

		[TestMethod]
		public void Returns_1_result_with_HasChildren_property_set_to_true_when_the_node_has_children()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			}, 
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.IsTrue(result.First().HasChildren);
		}

		[TestMethod]
		public void Returns_1_result_with_correct_id_set_from_node_found_by_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			}, 
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("2", result.First().Id);
		}

		[TestMethod]
		public void Returns_1_result_with_correct_name_set_from_node_found_by_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
				         			}, 
				         		new TreeNode()
				         			{
				         				ParentTreeNodeId = "1",
										Id = "2",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "2",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("fake tree node name", result.First().Name);
		}

		[TestMethod]
		public void Returns_2_results_when_the_specified_parent_node_has_2_child_nodes()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "2",
										Name = "fake tree node name",
									}, 
								new FakeTreeNode()
									{
										TreeNodeId = "3",
									}, 
				         	}.AsQueryable());
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "2",
										ParentTreeNodeId = "1",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "3",
				         				ParentTreeNodeId = "1",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void Returns_empty_set_when_passed_null()
		{
			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren(null);

			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public void Returns_1_result_when_the_specified_parent_node_has_2_child_nodes_but_only_1_is_found_by_provider()
		{
			var fakeTreeNodeExtensionProvider = new Mock<IAmATreeNodeExtensionProvider>();
			fakeTreeNodeExtensionProvider.Setup(a => a.GetAll())
				.Returns(new FakeTreeNode[]
				         	{
								new FakeTreeNode()
									{
										TreeNodeId = "1",
									}, 
				         	}.AsQueryable());
			mocker.GetMock<ITreeNodeProviderContext>().Setup(a => a.GetProviderByTypeName("FakeTreeNodeExtensionProvider"))
				.Returns(fakeTreeNodeExtensionProvider.Object);
			mocker.GetMock<ITreeNodeRepository>().Setup(a => a.GetAll())
				.Returns(new TreeNode[]
				         	{
				         		new TreeNode()
				         			{
										Id = "1",
										ParentTreeNodeId = "1",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
				         		new TreeNode()
				         			{
										Id = "2",
										ParentTreeNodeId = "1",
										Type = "FakeTreeNodeExtensionProvider",
				         			}, 
							}.AsQueryable());

			var treeNodeSummarContext = mocker.Resolve<TreeNodeSummaryContext>();
			var result = treeNodeSummarContext.GetChildren("1");

			Assert.AreEqual(1, result.Count());
		}
	}

	public class FakeTreeNode : IAmATreeNodeExtension
	{
		public string TreeNodeId { get; set; }
		public int? Sequence { get; set; }
		public string UrlSegment { get; set; }
		public string Name { get; set; }

		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
	}
}
