using System;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Data
{
	public class SectionNodeProviderDraft
	{
        public Guid SisoId { get; set; }

        public string SectionId { get; set; }

        public string TreeNodeId { get; set; }

		public int? Sequence { get; set; }

		public string Name { get; set; }

		public string UrlSegment { get; set; }

        public string DefaultTreeNodeId { get; set; }

		public bool Inactive { get; set; }
        
		public bool Hidden { get; set; }
	}
}
