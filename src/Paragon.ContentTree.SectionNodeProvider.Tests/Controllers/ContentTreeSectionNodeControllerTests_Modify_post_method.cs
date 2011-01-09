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
	}
}
