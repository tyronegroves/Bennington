using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.SectionNodeProvider.Mappers;

namespace Paragon.ContentTree.SectionNodeProvider.Tests.Mappers
{
	[TestClass]
	public class SectionNodeProviderDraftToContentTreeSectionNodeMapperTests
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
			var mapper = mocker.Resolve<SectionNodeProviderDraftToContentTreeSectionNodeMapper>();
			mapper.AssertConfigurationIsValid();
		}
	}
}
