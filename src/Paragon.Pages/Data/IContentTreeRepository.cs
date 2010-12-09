using System.Collections.Generic;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.ContentTree.Routing.Data
{
    public interface IContentTreeRepository
    {
        ContentTreeNode GetRootNode();
        IEnumerable<ContentTreeNode> GetChildNodesForNode(ContentTreeNode node);
    }
}