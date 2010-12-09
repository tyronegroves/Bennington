using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Paragon.Pages.Content.Json
{
    [DataContract]
    public class JsTreeNode
    {
        public JsTreeNode()
        {
            data = new JsTreeNodeData();
        }

        [DataMember]
        public object attr { get; set; }

        [DataMember]
        public JsTreeNodeData data { get; private set; }

        [DataMember]
        public string state { get; set; }

        [DataMember]
        public List<JsTreeNode> children { get; set; }
    }
}