namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Data
{
	public class ToolLinkProviderDraft
	{
        public bool Inactive { get; set; }
		public string Id { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string UrlSegment { get; set; }
		public string Url { get; set; }
		public bool Hidden { get; set; }
	}
}