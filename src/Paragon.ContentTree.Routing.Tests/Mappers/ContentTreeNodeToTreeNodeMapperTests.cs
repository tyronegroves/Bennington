using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.Routing.Mappers;

namespace Paragon.ContentTree.Routing.Tests.Mappers
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
