using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Domain.Commands;
using Paragon.ContentTree.SectionNodeProvider.Controllers;
using Paragon.ContentTree.SectionNodeProvider.Models;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.SectionNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeSectionNodeControllerTests_Modify_post_method
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_when_model_state_is_valid()
		{
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<ModifySectionCommand>()), Times.Once());
		}

		[TestMethod]
		public void Does_not_send_ModifySectionCommand_when_model_state_is_not_valid()
		{
			var controller = mocker.Resolve<ContentTreeSectionNodeController>();
			controller.ModelState.AddModelError("key", "error");

			controller.Modify(new ContentTreeSectionInputModel());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<ModifySectionCommand>()), Times.Never());
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_with_correct_DefaultTreeNodeId_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel()
			{
				Action = "action",
				DefaultTreeNodeId = defaultTreeNodeId,
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifySectionCommand>(b => b.DefaultTreeNodeId == defaultTreeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_with_correct_Name_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel()
			{
				Action = "action",
				Name = "name"
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifySectionCommand>(b => b.Name == "name")), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_with_correct_ParentTreeNodeId_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel()
			{
				Action = "action",
				ParentTreeNodeId = "parentId"
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifySectionCommand>(b => b.ParentTreeNodeId == "parentId")), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_with_correct_Sequence_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel()
			{
				Action = "action",
				Sequence = 1000,
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifySectionCommand>(b => b.Sequence == 1000)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_with_correct_UrlSegment_set_when_input_model_is_valid()
		{
			var defaultTreeNodeId = new Guid().ToString();
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel()
			{
				Action = "action",
				UrlSegment = "urlSegment",
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifySectionCommand>(b => b.UrlSegment == "urlSegment")), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifySectionCommand_with_correct_TreeNodeId_set_when_input_model_is_valid()
		{
			var treeNodeId = new Guid();
			mocker.Resolve<ContentTreeSectionNodeController>().Modify(new ContentTreeSectionInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "action",
				UrlSegment = "urlSegment",
			});

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifySectionCommand>(b => b.TreeNodeId == treeNodeId.ToString())), Times.Once());
		}
	}
}
