using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Controllers;
using Bennington.Cms.PrincipalProvider.Mappers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Bennington.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.PrincipalProvider.Tests.Controllers
{
	[TestClass]
	public class UserControlTests_Modify
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
			var result = mocker.Resolve<UserController>().Modify("id") as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder()
		{
			mocker.GetMock<IModifyViewModelBuilder>().Setup(a => a.BuildViewModel(null))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<UserController>().Modify("id") as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_instance_from_repository_into_mapper()
		{
			mocker.GetMock<IUserRepository>().Setup(a => a.GetAll())
				.Returns(new User[]
				         	{
				         		new User()
				         			{
				         				Id = "test",
				         			}, 
							});

			mocker.Resolve<UserController>().Modify("test");

			mocker.GetMock<IUserToUserInputModelMapper>()
				.Verify(a => a.CreateInstance(It.Is<User>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Passes_result_of_mapper_to_view_model_builder()
		{
			mocker.GetMock<IUserToUserInputModelMapper>().Setup(a => a.CreateInstance(It.IsAny<User>()))
				.Returns(new UserInputModel()
				         	{
				         		Id = "test"
				         	});
			mocker.GetMock<IUserRepository>().Setup(a => a.GetAll())
				.Returns(new User[]
				         	{
				         		new User()
				         			{
				         				Id = "test",
				         			}, 
							});

			mocker.Resolve<UserController>().Modify("test");

			mocker.GetMock<IModifyViewModelBuilder>()
				.Verify(a => a.BuildViewModel(It.Is<UserInputModel>(b => b.Id == "test")), Times.Once());
		}
	}
}
