using System;

namespace Bennington.ContentTree.Models
{
	public interface IAmATreeNodeExtension
	{
		string TreeNodeId { get; set; }
		int? Sequence { get; set; }
		string UrlSegment { get; set; }
		string Name { get; set; }
		bool Hidden { get; set; }
		bool Inactive { get; set; }
	    string IconUrl { get; set; }
        DateTime LastModifyDate { get; set; }
        string LastModifyBy { get; set; }
	}
}
