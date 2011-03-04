using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.PrincipalProvider.Tests.Validators
{
	[TestClass]
	public class UserInputModelValidatorTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_error_for_missing_username()
		{
			var result = mocker.Resolve<UserInputModelValidator>().Validate(new UserInputModel());

			Assert.IsTrue(result.Errors.Where(a => a.PropertyName == "Username").Count() > 0);
		}

		[TestMethod]
		public void Returns_error_for_mismatched_passwords()
		{
			var result = mocker.Resolve<UserInputModelValidator>().Validate(new UserInputModel()
			                                                                	{
			                                                                		ConfirmPassword = "x",
																					Password = "z",
			                                                                	});

			Assert.IsTrue(result.Errors.Where(a => a.PropertyName == "Password").Count() > 0);
			Assert.IsTrue(result.Errors.Where(a => a.PropertyName == "ConfirmPassword").Count() > 0);
		}
	}
}
