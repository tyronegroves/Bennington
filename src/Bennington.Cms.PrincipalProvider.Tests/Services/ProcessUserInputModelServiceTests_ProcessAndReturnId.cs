using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Mappers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;
using Bennington.Cms.PrincipalProvider.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.PrincipalProvider.Tests.Services
{
	[TestClass]
	public class ProcessUserInputModelServiceTests_ProcessAndReturnId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_SaveAndReturnId_method_of_user_repository_with_result_of_mapper()
		{
			mocker.GetMock<IUserInputModelToUserMapper>()
				.Setup(a => a.CreateInstance(It.Is<UserInputModel>(b => b.Id == "test")))
				.Returns(new User()
				         	{
				         		Id = "test"
				         	});

			mocker.Resolve<ProcessUserInputModelService>()
				.ProcessAndReturnId(new UserInputModel()
				                    	{
				                    		Id = "test",
				                    	});

			mocker.GetMock<IUserRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<User>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_result_from_SaveAndReturnId_method_of_user_repository()
		{
			mocker.GetMock<IUserRepository>()
				.Setup(a => a.SaveAndReturnId(It.IsAny<User>()))
				.Returns("test");

			var result = mocker.Resolve<ProcessUserInputModelService>().ProcessAndReturnId(new UserInputModel());

			Assert.AreEqual("test", result);
		}
	}
}
