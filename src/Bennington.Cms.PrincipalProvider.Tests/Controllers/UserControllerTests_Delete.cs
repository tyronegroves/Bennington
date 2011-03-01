using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Controllers;
using Bennington.Cms.PrincipalProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.PrincipalProvider.Tests.Controllers
{
	[TestClass]
	public class UserControllerTests_Delete
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_redirect()
		{
			var result = mocker.Resolve<UserController>().Delete("id") as RedirectToRouteResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Calls_Delete_method_of_repository_with_id()
		{
			mocker.Resolve<UserController>().Delete("id");

			mocker.GetMock<IUserRepository>()
				.Verify(a => a.Delete("id"), Times.Once());
		}
	}
}
