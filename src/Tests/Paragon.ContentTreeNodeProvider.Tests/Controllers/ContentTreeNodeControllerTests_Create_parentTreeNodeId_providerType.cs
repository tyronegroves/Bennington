using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Models;

namespace Paragon.ContentTreeNodeProvider.Tests.Controllers
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
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Create("2", null);

			Assert.AreEqual("2", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel.ParentTreeNodeId);
		}

		[TestMethod]
		public void Sets_view_model_action_to_create()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Create("", null);

			Assert.AreEqual("Create", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).Action);
		}

		[TestMethod]
		public void Sets_input_model_type_property_to_providerType_passed_in()
		{
			var landingPageController = mocker.Resolve<ContentTreeNodeController>();
			var result = landingPageController.Create("", "provider type");

			Assert.AreEqual("provider type", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel.Type);
		}
	}
}
