using System;
using System.Collections.Generic;
using System.Linq;
using Paragon.Pages.Content.Json;
using Paragon.Pages.Routing;

namespace Paragon.Pages.Content
{
    public class ContentTree
    {
        private readonly ContentTreeNode rootNode;

        public ContentTree(ContentTreeNode rootNode)
        {
            this.rootNode = rootNode;
        }

        public ContentTreeNode RootNode
        {
            get { return rootNode; }
        }

        public int MaxDepth
        {
            get { return GetAllContentTreeNodesForContentRouting(rootNode).Max(node => node.Depth); }
        }

		//public IEnumerable<JsTreeNode> GetChildNodes(string nodeId)
		//{
		//    return rootNode.NodeId == nodeId
		//               ? rootNode.ChildNodes.Select(node => node.GetJsTreeNode())
		//               : rootNode.Single(node => node.NodeId == nodeId).ChildNodes.Select(node => node.GetJsTreeNode());
		//}

        private static IEnumerable<ContentTreeNode> GetAllContentTreeNodesForContentRouting(ContentTreeNode node)
        {
            if(node is IWantCustomRouting)
                return new List<ContentTreeNode>();

            var nodes = new List<ContentTreeNode> {node};

            foreach(var childNode in node)
                nodes.AddRange(GetAllContentTreeNodesForContentRouting(childNode));

            return nodes;
        }
    }
}