using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.Pages.Mappers;

namespace Paragon.Pages.Tests.Mappers
{
	[TestClass]
	public class ContentTreeNodeToTreeNodeMapperTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void INit()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Assert_configuration_is_valid()
		{
			var mapper = mocker.Resolve<ContentTreeNodeToTreeNodeMapper>();
			mapper.AssertConfigurationIsValid();
		}
	}
}
