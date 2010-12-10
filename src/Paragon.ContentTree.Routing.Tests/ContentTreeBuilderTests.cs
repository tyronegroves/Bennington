using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.Routing.Content;
using Paragon.ContentTree.Routing.Data;

namespace Paragon.ContentTree.Routing.Tests
{
    [TestClass]
    public class ContentTreeBuilderTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void SetupMocksForAllTests()
        {
            mocker = new AutoMoqer();

            mocker.GetMock<IContentTreeRepository>()
                .Setup(repository => repository.GetRootNode())
                .Returns(new MockContentTreeNode("root"));

            mocker.GetMock<IContentTreeRepository>()
                .Setup(repository => repository.GetChildNodesForNode(It.IsAny<ContentTreeNode>()))
                .Returns(new ContentTreeNodeCollection(new MockContentTreeNode("rootOfCollection")));
        }

        [TestMethod]
        public void RootNodeFromContentTreeRepositoryIsReturnedWhenGetRootNodeIsCalled()
        {
            var builder = CreateContentTreeBuilder();
            var exceptedNode = new MockContentTreeNode("");

            mocker.GetMock<IContentTreeRepository>()
                .Setup(repository => repository.GetRootNode())
                .Returns(exceptedNode);

            var contentTree = builder.GetContentTree();
            var rootNode = contentTree.RootNode;

            Assert.AreSame(exceptedNode, rootNode);
        }

        [TestMethod]
        public void RootNodeChildNodesFromContentTreeRepositoryAreReturnedWhenGetRootNodeIsCalled()
        {
            var builder = CreateContentTreeBuilder();
            var node = new MockContentTreeNode("root");
            var node1 = new MockContentTreeNode("root/node1");

            mocker.GetMock<IContentTreeRepository>()
                .Setup(repository => repository.GetChildNodesForNode(node))
                .Returns(new[] {node1});

            mocker.GetMock<IContentTreeRepository>()
                .Setup(repository => repository.GetRootNode())
                .Returns(node);

            var contentTree = builder.GetContentTree();
            var rootNode = contentTree.RootNode;

            Assert.AreSame(node1, rootNode.ChildNodes[0]);
        }

		//[TestMethod]
		//public void ChildNodesChildrenFromContentTreeRepositoryAreReturnedWhenGetRootNodeIsCalled()
		//{
		//    var builder = CreateContentTreeBuilder();
		//    var node = new MockContentTreeNode("root");
		//    var node1 = new MockContentTreeNode("root/node");
		//    var node2 = new MockContentTreeNode("root/node/node");

		//    mocker.GetMock<IContentTreeRepository>()
		//        .Setup(repository => repository.GetRootNode())
		//        .Returns(node);

		//    mocker.GetMock<IContentTreeRepository>()
		//        .Setup(repository => repository.GetChildNodesForNode(node2))
		//        .Returns(new ContentTreeNode[] {});

		//    mocker.GetMock<IContentTreeRepository>()
		//        .Setup(repository => repository.GetChildNodesForNode(node1))
		//        .Returns(new[] {node2});

		//    mocker.GetMock<IContentTreeRepository>()
		//        .Setup(repository => repository.GetChildNodesForNode(node))
		//        .Returns(new[] {node1});

		//    var contentTree = builder.GetContentTree();
		//    var rootNode = contentTree.RootNode;

		//    Assert.AreEqual("root/node", rootNode.ChildNodes[0].NodeId);
		//    Assert.AreSame(node1, rootNode.ChildNodes[0]);
		//    Assert.AreSame(node2, rootNode.ChildNodes[0].ChildNodes[0]);
		//}

        public ContentTreeBuilder CreateContentTreeBuilder()
        {
            return mocker.Resolve<ContentTreeBuilder>();
        }
    }
}