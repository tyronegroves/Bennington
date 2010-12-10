using Paragon.ContentTree.Routing.Content;

namespace Paragon.ContentTree.Routing.Tests
{
    public class MockContentTreeNode : ContentTreeNode
    {
        public MockContentTreeNode(string nodeId) : base(/*nodeId*/)
        {
        }

		//public override JsTreeNode GetJsTreeNode()
		//{
		//    throw new NotImplementedException();
		//}
    }
}