using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Controllers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.PrincipalProvider.Tests.Controllers
{
	[TestClass]
	public class UserControllerTests_Index
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
			var result = mocker.Resolve<UserController>().Index() as ViewResult;

			Assert.AreEqual("Index", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder()
		{
			mocker.GetMock<IIndexViewModelBuilder>()
				.Setup(a => a.BuildViewModel())
				.Returns(new IndexViewModel());

			var result = mocker.Resolve<UserController>().Index() as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as IndexViewModel);
		}
	}
}
