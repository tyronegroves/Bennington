using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Contexts;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Data;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Tests.Contexts
{
	[TestClass]
	public class ToolLinkProviderTests_GetAllToolLinks
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_tool_links_from_ToolLinkProviderDraftToToolLinkMapper()
		{
			mocker.GetMock<IToolLinkProviderDraftToToolLinkMapper>().Setup(a => a.CreateSet(It.IsAny<IEnumerable<ToolLinkProviderDraft>>()))
				.Returns(new ToolLink[]
				         	{
				         		new ToolLink()
				         			{
				         				Name = "test"
				         			}, 
							});

			var result = mocker.Resolve<ToolLinkContext>().GetAllToolLinks();

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("test", result.First().Name);
		}

		[TestMethod]
		public void Passes_data_from_repository_into_mapper()
		{
			mocker.GetMock<IToolLinkProviderDraftRepository>().Setup(a => a.GetAll())
				.Returns(new ToolLinkProviderDraft[]
				         	{
				         		new ToolLinkProviderDraft()
				         			{
				         				Name = "test",
				         			}, 
							}.AsQueryable());

			mocker.Resolve<ToolLinkContext>().GetAllToolLinks();

			mocker.GetMock<IToolLinkProviderDraftToToolLinkMapper>().Verify(a => a.CreateSet(It.Is<IEnumerable<ToolLinkProviderDraft>>(b => b.First().Name == "test")), Times.Once());
		}
	}
}
