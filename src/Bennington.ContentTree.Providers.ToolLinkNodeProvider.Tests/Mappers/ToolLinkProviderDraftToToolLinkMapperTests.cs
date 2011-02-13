using AutoMapperAssist;
using AutoMoq;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Tests.Mappers
{
	[TestClass]
	public class ToolLinkProviderDraftToToolLinkMapperTests
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
			var mapper = mocker.Resolve<ToolLinkProviderDraftToToolLinkMapper>();
			mapper.AssertConfigurationIsValid();
		}
	}
}
