using System.Collections.Generic;
using Paragon.Pages.Content;

namespace Paragon.Pages.Data
{
    public interface IContentTreeRepository
    {
        ContentTreeNode GetRootNode();
        IEnumerable<ContentTreeNode> GetChildNodesForNode(ContentTreeNode node);
    }
}