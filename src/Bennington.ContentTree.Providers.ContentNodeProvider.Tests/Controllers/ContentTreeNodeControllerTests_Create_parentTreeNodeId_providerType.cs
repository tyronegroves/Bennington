using System.Web.Mvc;
using AutoMoq;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerTests_Create_parentTreeNodeId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_view_model_with_input_model_ParentTreeNodeId_property_set_to_ParentTreeNodeId_passed_in()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Create("2", null);

			Assert.AreEqual("2", ((ModifyViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel.ParentTreeNodeId);
		}

		[TestMethod]
		public void Sets_view_model_action_to_create()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Create("", null);

			Assert.AreEqual("Create", ((ModifyViewModel)((ViewResult)result).ViewData.Model).Action);
		}

		[TestMethod]
		public void Sets_input_model_type_property_to_providerType_passed_in()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Create("", "provider type");

			Assert.AreEqual("provider type", ((ModifyViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel.Type);
		}
	}
}
