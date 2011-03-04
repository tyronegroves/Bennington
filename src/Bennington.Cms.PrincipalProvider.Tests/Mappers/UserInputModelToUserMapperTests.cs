using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Encryption;
using Bennington.Cms.PrincipalProvider.Mappers;
using Bennington.Cms.PrincipalProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.PrincipalProvider.Tests.Mappers
{
	[TestClass]
	public class UserInputModelToUserMapperTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Assert_configuration_is_valid()
		{
			var mapper = mocker.Resolve<UserInputModelToUserMapper>();
			mapper.AssertConfigurationIsValid();
		}

		[TestMethod]
		public void Encrypts_password()
		{
			mocker.GetMock<IEncryptionService>().Setup(a => a.Encrypt(It.IsAny<string>())).Returns("encrypted");

			var result = mocker.Resolve<UserInputModelToUserMapper>().CreateInstance(new UserInputModel()
			                                                                         	{
			                                                                         		Password = "test",
			                                                                         	});

			Assert.AreEqual("encrypted", result.Password);
		}
	}
}
