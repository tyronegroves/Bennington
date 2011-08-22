﻿using System;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Models
{
	public class ContentTreeNode : IAmATreeNodeExtension
	{
	    private string pathToIcon = "/MANAGE/Content/ContentNodeProvider/page.gif";
		public string TreeNodeId { get; set; }
		public string PageId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string Body { get; set; }
		public string Action { get; set; }
		public string HeaderText { get; set; }
		public string HeaderImage { get; set; }
		public bool Inactive { get; set; }

	    public string IconUrl
	    {
            get { return pathToIcon; }
	        set { pathToIcon = value; }
	    }

	    public bool Hidden { get; set; }
	}
}
