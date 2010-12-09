using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTreeNodeProvider.Context;
using Paragon.ContentTreeNodeProvider.Data;
using Paragon.ContentTreeNodeProvider.Repositories;

namespace Paragon.ContentTreeNodeProvider.Tests.Context
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
