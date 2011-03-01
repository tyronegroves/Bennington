using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Controllers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.PrincipalProvider.Tests.Controllers
{
	[TestClass]
	public class UserControllerTests_Create
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_view_name()
		{
			var result = mocker.Resolve<UserController>().Create() as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_model_with_id_property_of_UserInputModel_set_from_IGuidGetter()
		{
			var guid = Guid.NewGuid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid())
				.Returns(guid);

			var result = mocker.Resolve<UserController>().Create() as ViewResult;

			var viewModel = result.ViewData.Model as ModifyViewModel;
			Assert.AreEqual(guid.ToString(), viewModel.UserInputModel.Id);
		}
	}
}
