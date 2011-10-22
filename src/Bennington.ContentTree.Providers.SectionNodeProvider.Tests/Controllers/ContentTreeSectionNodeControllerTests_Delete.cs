using System;
using System.Security.Principal;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Providers.SectionNodeProvider.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeSectionNodeControllerTests_Delete
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
            mocker.GetMock<ICurrentUserContext>()
                .Setup(a => a.GetCurrentPrincipal())
                .Returns(new GenericPrincipal(new GenericIdentity("test"), new string[] { }));

		}

		[TestMethod]
		public void Sends_DeleteTreeNodeCommand()
		{
			var guid = new Guid();

			mocker.Resolve<ContentTreeSectionNodeController>().Delete(guid.ToString());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeleteTreeNodeCommand>(b => b.AggregateRootId == guid)), Times.Once());
		}

		[TestMethod]
		public void Sends_DeleteSectionCommand()
		{
			var guid = new Guid();

			mocker.Resolve<ContentTreeSectionNodeController>().Delete(guid.ToString());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeleteSectionCommand>(b => b.AggregateRootId == guid)), Times.Once());
		}

        [TestMethod]
        public void Sends_DeleteSectionCommand_with_LastModifyBy_set()
        {
            var guid = new Guid();

            mocker.Resolve<ContentTreeSectionNodeController>().Delete(guid.ToString());

            mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeleteSectionCommand>(b => b.LastModifyBy == "test")), Times.Once());
        }
	}
}
