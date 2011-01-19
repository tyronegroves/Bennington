using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Context
{
	[TestClass]
	public class ContentTreeNodeContextTests_Delete
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker= new AutoMoqer();
		}

		[TestMethod]
		public void Does_not_Call_Delete_method_of_IContentTreeNodeRepository_when_node_with_matching_id_is_not_found()
		{
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes())
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = "id2",
				         			}, 
							}.AsQueryable());

			var ContentTreeNodeContext = mocker.Resolve<ContentTreeNodeContext>();
			ContentTreeNodeContext.Delete("id");

			mocker.GetMock<IContentTreeNodeRepository>().Verify(a => a.Delete(It.Is<ContentTreeNode>(b => b.TreeNodeId == "id")), Times.Never());
		}

		[TestMethod]
		public void Calls_Delete_method_of_IContentTreeNodeRepository_with_node_with_matching_id()
		{
			mocker.GetMock<IContentTreeNodeRepository>().Setup(a => a.GetAllContentTreeNodes())
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = "id",
				         			}, 
							}.AsQueryable());

			var ContentTreeNodeContext = mocker.Resolve<ContentTreeNodeContext>();
			ContentTreeNodeContext.Delete("id");

			mocker.GetMock<IContentTreeNodeRepository>().Verify(a => a.Delete(It.Is<ContentTreeNode>(b => b.TreeNodeId == "id")), Times.Once());
		}
	}
}
