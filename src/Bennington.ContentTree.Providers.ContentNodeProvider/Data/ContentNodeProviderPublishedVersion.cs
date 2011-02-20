namespace Bennington.ContentTree.Providers.ContentNodeProvider.Data
{
	public class ContentNodeProviderPublishedVersion
	{
		public string PageId { get; set; }
		public string TreeNodeId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string Action { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public string MetaKeyword { get; set; }
		public string HeaderText { get; set; }
		public string HeaderImage { get; set; }
		public string Body { get; set; }
		public bool Inactive { get; set; }
		public bool Hidden { get; set; }
	}
}
