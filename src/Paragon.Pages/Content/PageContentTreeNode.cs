using Paragon.Pages.Content.Json;

namespace Paragon.Pages.Content
{
    public class PageContentTreeNode : ContentTreeNode
    {
        public PageContentTreeNode(string nodeId) : base(nodeId)
        {
        }

        public string Title { get; set; }

        public override JsTreeNode GetJsTreeNode()
        {
            return new JsTreeNode {attr = new {nodeid = NodeId}, data = {title = Title, icon = "page" }};
        }
    }
}