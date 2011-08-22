namespace Bennington.ContentTree.Models
{
	public class TreeNodeSummary
	{
		public string ParentTreeNodeId { get; set; }
		public string Name { get; set; }
		public string Id { get; set; }
		public string UrlSegment { get; set; }
		public bool HasChildren { get; set; }
		public string ControllerToUseForCreation { get; set; }
		public string ActionToUseForCreation { get; set; }
		public string ControllerToUseForModification { get; set; }
		public string ActionToUseForModification { get; set; }
		public object RouteValuesForModification { get; set; }
		public object RouteValuesForCreation { get; set; }
		public int? Sequence { get; set; }
		public string Type { get; set; }
        public string IconUrl { get; set; }
		public bool MayHaveChildNodes { get; set; }
		public bool Hidden { get; set; }
	}
}