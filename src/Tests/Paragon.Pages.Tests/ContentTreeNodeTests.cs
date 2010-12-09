using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.Pages.Tests
{
    [TestClass]
    public class ContentTreeNodeTests
    {
		//[TestMethod]
		//public void NodeIdPropertyIsSetToNodeIdFromConstructor()
		//{
		//    var node = new MockContentTreeNode("nodeId1");

		//    Assert.AreEqual("nodeId1", node.NodeId);
		//}

        [TestMethod]
        public void ParentPropertyCanBeSetAndRetieved()
        {
            var node = new MockContentTreeNode("nodeId");
            var parent = new MockContentTreeNode("parent");

            node.Parent = parent;

            Assert.AreEqual(parent, node.Parent);
        }

        [TestMethod]
        public void ParentIsAssignedToContentTreeNodeWhenAddedToTheCollection()
        {
            var parentNode = new MockContentTreeNode("root");
            var collection = new ContentTreeNodeCollection(parentNode);

            var node = new MockContentTreeNode("node");
            collection.Add(node);

            Assert.AreSame(parentNode, node.Parent);
        }

        [TestMethod]
        public void ParentIsAssignedToContentTreeNodeWhenInsertedInToTheCollection()
        {
            var parentNode = new MockContentTreeNode("root");
            var collection = new ContentTreeNodeCollection(parentNode);

            var node = new MockContentTreeNode("node");
            collection.Insert(0, node);

            Assert.AreSame(parentNode, node.Parent);
        }

        [TestMethod]
        public void ParentIsAssignedToContentTreeNodeWhenSetInTheCollection()
        {
            var parentNode = new MockContentTreeNode("root");
            var collection = new ContentTreeNodeCollection(parentNode);

            var node = new MockContentTreeNode("node");
            collection.Add(new MockContentTreeNode("node2"));
            collection[0] = node;

            Assert.AreSame(parentNode, node.Parent);
        }
    }
}