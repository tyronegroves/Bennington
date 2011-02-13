using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Contexts;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Tests
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

			var results = mocker.Resolve<Bennington.ContentTree.Providers.ToolLinkNodeProvider.ToolLinkNodeProvider>().GetAll();

			Assert.AreEqual(1, results.Count());
			Assert.AreEqual("test", results.First().Name);
		}
	}
}
