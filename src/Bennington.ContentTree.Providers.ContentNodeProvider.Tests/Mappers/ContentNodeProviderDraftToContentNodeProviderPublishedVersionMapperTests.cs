using AutoMapperAssist;
using AutoMoq;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Mappers
{
	[TestClass]
	public class ContentNodeProviderDraftToContentNodeProviderPublishedVersionMapperTests
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
			var mapper = mocker.Resolve<ContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>();
			mapper.AssertConfigurationIsValid();
		}
	}
}
