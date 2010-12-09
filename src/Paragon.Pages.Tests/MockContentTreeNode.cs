using System;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.Pages.Tests
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