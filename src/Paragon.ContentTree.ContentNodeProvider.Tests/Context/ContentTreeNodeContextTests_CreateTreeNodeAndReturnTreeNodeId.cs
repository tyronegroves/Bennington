using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Context
{
	[TestClass]
	public class ContentTreeNodeContextTests_CreateTreeNodeAndReturnTreeNodeId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Create_method_of_ITreeNodeSummaryContext_with_type_from_input_model()
		{
			var contentTreeNodeContext = mocker.Resolve<ContentTreeNodeContext>();
			var result = contentTreeNodeContext.CreateTreeNodeAndReturnTreeNodeId(new ContentTreeNodeInputModel()
			                                                                      	{
			                                                                      		ParentTreeNodeId = "parentTreeNodeId",
																						Type = "provider type",
			                                                                      	});
			mocker.GetMock<ITreeNodeSummaryContext>().Verify(a => a.Create("parentTreeNodeId", "provider type"), Times.Once());
		}

		[TestMethod]
		public void Returns_newly_created_tree_node_id()
		{
			mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.Create(It.Is<string>(b => b == "parentTreeNodeId"), It.IsAny<string>())).Returns("newTreeNodeId");

			var ContentTreeNodeContext = mocker.Resolve<ContentTreeNodeContext>();
			var result = ContentTreeNodeContext.CreateTreeNodeAndReturnTreeNodeId(new ContentTreeNodeInputModel()
																						{
																							ParentTreeNodeId = "parentTreeNodeId",
																						});

			Assert.AreEqual("newTreeNodeId", result);
		}

		//[TestMethod]
		//public void Sets_TreeNodeId_property_of_input_model_to_newly_created_tree_node_id_returned_from_ITreeNodeSummaryContext_before_calling_mapper()
		//{
		//    mocker.GetMock<ITreeNodeSummaryContext>().Setup(a => a.Create(It.Is<string>(b => b == "parentTreeNodeId"), It.IsAny<string>())).Returns("newTreeNodeId");

		//    var ContentTreeNodeContext = mocker.Resolve<ContentTreeNodeContext>();
		//    ContentTreeNodeContext.CreateTreeNodeAndReturnTreeNodeId(new ContentTreeNodeInputModel()
		//                                                                            {
		//                                                                                ParentTreeNodeId = "parentTreeNodeId",
		//                                                                            });

		//    mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Verify(a => a.CreateInstance(It.Is<ContentTreeNodeInputModel>(b => b.TreeNodeId == "newTreeNodeId")), Times.Once());
		//}

		//[TestMethod]
		//public void Calls_CreateTreeNodeAndReturnTreeNodeId_method_of_IContentTreeNodeRepository_with_object_returned_from_mapper()
		//{
		//    var ContentTreeNode = new ContentTreeNode()
		//                                        {
		//                                            Name = "Name",
		//                                        };

		//    mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>().Setup(
		//        a => a.CreateInstance(It.IsAny<ContentTreeNodeInputModel>()))
		//        .Returns(ContentTreeNode);

		//    var ContentTreeNodeContext = mocker.Resolve<ContentTreeNodeContext>();
		//    ContentTreeNodeContext.CreateTreeNodeAndReturnTreeNodeId(new ContentTreeNodeInputModel());

		//    mocker.GetMock<IContentTreeNodeVersionContext>().Verify(a => a.Create(ContentTreeNode), Times.Once());
		//}
	}
}
