using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.ToolLinkNodeProvider.Contexts;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Tests
{
	[TestClass]
	public class ToolLinkNodeProviderTests_GetAll
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_tool_links_from_IToolLinkContext()
		{
			mocker.GetMock<IToolLinkContext>().Setup(a => a.GetAllToolLinks())
				.Returns(new ToolLink[]
				         	{
				         		new ToolLink()
				         			{
				         				Name = "test"
				         			}, 
							});

			var results = mocker.Resolve<ToolLinkNodeProvider>().GetAll();

			Assert.AreEqual(1, results.Count());
			Assert.AreEqual("test", results.First().Name);
		}
	}
}
