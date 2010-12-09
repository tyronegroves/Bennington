//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMoq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Paragon.ContentTree.Data;
//using Paragon.Pages.Adapters;
//using Paragon.Pages.Content;
//using Paragon.Pages.Data;
//using Paragon.Pages.Mappers;

//namespace Paragon.Pages.Tests.Adapters
//{
//    [TestClass]
//    public class NodeSegmentEnumerableToTreeNodeAdapterTests
//    {
//        private AutoMoqer mocker;

//        [TestInitialize]
//        public void INit()
//        {
//            mocker = new AutoMoqer();
//        }

//        [TestMethod]
//        public void Returns_correct_TreeNode_for_url_1_level_deep()
//        {
//            var childNodes = new ContentTreeNodeCollection(null);
//            childNodes.Add(new ContentTreeNode()
//                            {
//                                UrlSegment = "urlSegment1",
//                                NodeId = "nodeId",
//                            });
//            mocker.GetMock<IContentTreeRepository>().Setup(a => a.GetRootNode())
//                .Returns(new ContentTreeNode()
//                            {
//                                ChildNodes = childNodes,
//                            });
//            mocker.GetMock<IContentTreeNodeToTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.NodeId == "nodeId")))
//                .Returns(new TreeNode()
//                            {
//                                Id = "nodeId",
//                            });

//            var result = mocker.Resolve<NodeSegmentEnumerableToTreeNodeAdapter>().GetTreeNodeFromUrl(new string[]
//                                                                                                        {
//                                                                                                            "urlSegment1",
//                                                                                                        });

//            Assert.AreEqual("nodeId", result.Id);
//        }

//        [TestMethod]
//        public void Returns_correct_TreeNode_for_url_2_levels_deep()
//        {
//            var rootNode = new ContentTreeNode()
//                            {
//                                UrlSegment = "/",
//                                NodeId = "nodeId",
//                            };
//            var rootChildren = new ContentTreeNodeCollection(rootNode);
//            rootChildren.Add(new ContentTreeNode()
//                                        {
//                                            NodeId = "childNodeId",
//                                            UrlSegment = "urlSegment1",
//                                            ChildNodes = new ContentTreeNodeCollection(null)
//                                                            {
//                                                                new ContentTreeNode()
//                                                                    {
//                                                                        UrlSegment = "urlSegment2",
//                                                                        NodeId = "grandchildNodeId"
//                                                                    }
//                                                            }
//                                        });
//            rootNode.ChildNodes = rootChildren;
//            mocker.GetMock<IContentTreeRepository>().Setup(a => a.GetRootNode()).Returns(rootNode);
//            mocker.GetMock<IContentTreeNodeToTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.NodeId == "nodeId")))
//                .Returns(new TreeNode()
//                {
//                    Id = "nodeId",
//                });
//            mocker.GetMock<IContentTreeNodeToTreeNodeMapper>().Setup(a => a.CreateInstance(It.Is<ContentTreeNode>(b => b.NodeId == "grandchildNodeId")))
//                .Returns(new TreeNode()
//                {
//                    Id = "grandchildNodeId",
//                });


//            var result = mocker.Resolve<NodeSegmentEnumerableToTreeNodeAdapter>().GetTreeNodeFromUrl(new string[]
//                                                                                                        {
//                                                                                                            "urlSegment1",
//                                                                                                            "urlSegment2",
//                                                                                                        });

//            Assert.AreEqual("grandchildNodeId", result.Id);
//        }
//    }
//}
