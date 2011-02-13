using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ToolLinkNodeProvider.Contexts;
using Paragon.ContentTree.ToolLinkNodeProvider.Data;
using Paragon.ContentTree.ToolLinkNodeProvider.Mappers;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;
using Paragon.ContentTree.ToolLinkNodeProvider.Repositories;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Tests
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

			mocker.GetMock<IToolLinkProviderDraftToToolLinkMapper>().Verify(a => a.CreateSet(It.Is<IEnumerable<Data.ToolLinkProviderDraft>>(b => b.First().Name == "test")), Times.Once());
		}
	}
}
