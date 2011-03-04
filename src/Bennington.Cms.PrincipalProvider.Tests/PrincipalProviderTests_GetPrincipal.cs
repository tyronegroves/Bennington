using System;
using System.Security.Principal;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Encryption;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Cms.PrincipalProvider.Tests
{
	[TestClass]
	public class PrincipalProviderTests_GetPrincipal
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			mocker.GetMock<IEncryptionService>().Setup(a => a.Encrypt(It.IsAny<string>())).Returns("password");
		}

		[TestMethod]
		public void Returns_null_principal_when_principal_doesnt_exist()
		{
			var result = mocker.Resolve<PrincipalProvider>().GetPrincipal("userId", "password");

			Assert.IsNull(result.Principal);
		}

		[TestMethod]
		public void Returns_principal_when_principal_exists()
		{
			mocker.GetMock<IUserRepository>().Setup(a => a.GetAll())
				.Returns(new User[]
				         	{
				         		new User()
				         			{
				         				Username = "user",
										Password = "password",
				         			}, 
							});

			var result = mocker.Resolve<PrincipalProvider>().GetPrincipal("user", "password");

			var principal = (GenericPrincipal) result.Principal;
			Assert.AreEqual("user", principal.Identity.Name);
		}

		[TestMethod]
		public void Returns_null_principal_when_user_id_matches_but_password_doesnt()
		{
			mocker.GetMock<IUserRepository>().Setup(a => a.GetAll())
				.Returns(new User[]
				         	{
				         		new User()
				         			{
				         				Username = "user",
										Password = "password",
				         			}, 
							});

			var result = mocker.Resolve<PrincipalProvider>().GetPrincipal("user", "test");

			Assert.IsNull(result.Principal);
		}

	}
}
