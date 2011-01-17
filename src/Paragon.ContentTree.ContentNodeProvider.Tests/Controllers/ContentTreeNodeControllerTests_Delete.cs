using System;
using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class contentTreeNodeControllerTests_Delete
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		//[TestMethod]
		//public void Calls_delete_on_IContentTreeNodeContext_with_correct_id()
		//{
		//    var guid = new Guid();

		//    var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
		//    contentTreeNodeController.Delete(guid.ToString());

		//    mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.Delete(guid.ToString()), Times.Once());
		//}

		[TestMethod]
		public void Returns_redirect_to_ContentTreeController_Index_action()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Delete(new Guid().ToString());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			var redirectToRouteResult = (RedirectToRouteResult) result;
			//Assert.AreEqual(1, redirectToRouteResult.RouteValues.ToArray().Where(a => a.Key == "controller").Where(a => a.Value == "ContentTree").Count());
			//Assert.AreEqual(1, redirectToRouteResult.RouteValues.Where(a => a.Key == "action").Where(a => a.Value == "index").Count());
		}

		[TestMethod]
		public void Sends_DeletePageCommand()
		{
			var treeNodeId = new Guid();

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.Delete(treeNodeId.ToString());

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<DeletePageCommand>(b => b.AggregateRootId == treeNodeId)), Times.Once());
		}
	}
}
