using AutoMapperAssist;
using AutoMoq;
using Bennington.ContentTree.Providers.SectionNodeProvider.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Tests.Mappers
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
