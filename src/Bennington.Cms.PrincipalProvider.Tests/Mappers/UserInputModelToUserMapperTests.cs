using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Mappers;
using Bennington.Cms.PrincipalProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
	}
}
