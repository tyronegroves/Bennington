using System;
using Paragon.Pages.Content;
using Paragon.Pages.Content.Json;

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