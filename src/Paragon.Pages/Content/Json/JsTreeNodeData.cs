using System.Runtime.Serialization;

namespace Paragon.Pages.Content.Json
{
    [DataContract]
    public class JsTreeNodeData
    {
        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string icon { get; set; }

        [DataMember]
        public object attr { get; set; }
    }
}