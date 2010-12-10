using Paragon.Pages.Content.Json;

namespace Paragon.Pages.Content
{
    public class SectionContentTreeNode : ContentTreeNode
    {
        public SectionContentTreeNode(string nodeId) : base(nodeId)
        {
        }

        public string Title { get; set; }

        public override JsTreeNode GetJsTreeNode()
        {
            return new JsTreeNode { attr = new { nodeid = NodeId }, data = { title = Title, icon = "section" }, state = ChildNodes.Count == 0 ? "open" : "closed" };
        }
    }
}