using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Domain.Commands;
using Paragon.ContentTree.SectionNodeProvider.Controllers;
using Paragon.ContentTree.SectionNodeProvider.Models;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.SectionNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeSectionNodeControllerTests_Create_post_method
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_when_input_model_is_valid()
		{
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
			                                                          	{
			                                                          		Action = "action"
			                                                          	});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<CreateSectionCommand>()), Times.Once());
		}

		[TestMethod]
		public void Does_not_send_CreateSectionCommand_when_input_model_is_not_valid()
		{
			var controller = mocker.Resolve<ContentTreeSectionNodeController>();
			controller.ModelState.AddModelError("key", "error");

			controller.Create(new ContentTreeSectionInputModel()
										{
											Action = "action"
										});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<CreateSectionCommand>()), Times.Never());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_correct_DefaultTreeNodeId_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
																			{
																				Action = "action",
																				DefaultTreeNodeId = defaultTreeNodeId,
																			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.DefaultTreeNodeId == defaultTreeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_correct_Name_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
																			{
																				Action = "action",
																				Name = "name"
																			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.Name == "name")), Times.Once());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_correct_ParentTreeNodeId_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
			{
				Action = "action",
				ParentTreeNodeId = "parentId"
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.ParentTreeNodeId == "parentId")), Times.Once());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_correct_Sequence_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
			{
				Action = "action",
				Sequence = 1000,
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.Sequence == 1000)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_correct_UrlSegment_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
			{
				Action = "action",
				UrlSegment = "urlSegment",
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.UrlSegment == "urlSegment")), Times.Once());
		}

		[TestMethod]
		public void Calls_Create_method_of_ITreeNodeSummaryContext_with_proper_args_when_input_model_is_valid()
		{
			var parentNodeId = Guid.NewGuid();
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
																			{
																				Action = "action",
																				ParentTreeNodeId = parentNodeId.ToString(),
																			});

			mocker.GetMock<ITreeNodeSummaryContext>().Verify(a => a.Create(parentNodeId.ToString(), typeof(SectionNodeProvider).AssemblyQualifiedName), Times.Once());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_tree_node_id_returned_by_ITreeNodeSummaryContext()
		{
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid()).Returns(Guid.NewGuid());
			var treeNodeId = new Guid().ToString();
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.Create(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(treeNodeId.ToString());
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
			{
				Action = "action",
				UrlSegment = "urlSegment",
				SectionId = Guid.NewGuid().ToString()
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.TreeNodeId == treeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreateSectionCommand_with_section_id_returned_by_IGuidGetter()
		{
			var id = new Guid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid()).Returns(id);
			mocker.Resolve<ContentTreeSectionNodeController>().Create(new ContentTreeSectionInputModel()
			{
				Action = "action",
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreateSectionCommand>(b => b.SectionId == id.ToString())), Times.Once());
		}
	}
}
