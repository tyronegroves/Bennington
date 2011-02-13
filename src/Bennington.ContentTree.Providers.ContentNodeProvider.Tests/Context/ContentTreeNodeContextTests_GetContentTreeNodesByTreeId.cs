using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Context
{
	[TestClass]
	public class ContentTreeNodeContextTests_GetContentTreeNodesByTreeId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_ContentTreeNode_from_IContentTreeNodeRepository()
		{
			mocker.GetMock<IContentTreeNodeVersionContext>().Setup(a => a.GetAllContentTreeNodes()).Returns(
				new ContentTreeNode[]
					{
						new ContentTreeNode()
							{
								Body = "content",
								TreeNodeId = "id",
							}, 
					}.AsQueryable());

			var result = mocker.Resolve<ContentTreeNodeContext>().GetContentTreeNodesByTreeId("id");

			Assert.AreEqual("content", result.First().Body);
		}
	}
}
