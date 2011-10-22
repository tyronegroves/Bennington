using System;
using System.Security.Principal;
using System.Web.Mvc;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class contentTreeNodeControllerTests_Delete
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
		public void Returns_redirect_to_ContentTreeController_Index_action()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Delete(new Guid().ToString());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void Sends_DeletePageCommand()
		{
			var treeNodeId = new Guid();

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.Delete(treeNodeId.ToString());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeletePageCommand>(b => b.AggregateRootId == treeNodeId)), Times.Once());
		}

        [TestMethod]
        public void Sends_DeletePageCommand_with_LastModifyBy_set()
        {
            var treeNodeId = new Guid();

            var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
            contentTreeNodeController.Delete(treeNodeId.ToString());

            mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeletePageCommand>(b => b.LastModifyBy == "test")), Times.Once());
        }

		[TestMethod]
		public void Sends_DeleteTreeNodeCommand()
		{
			var treeNodeId = new Guid();

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.Delete(treeNodeId.ToString());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeleteTreeNodeCommand>(b => b.AggregateRootId == treeNodeId)), Times.Once());
		}
	}
}
