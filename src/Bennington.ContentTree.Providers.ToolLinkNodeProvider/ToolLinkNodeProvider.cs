using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.ToolLinkNodeProvider.Contexts;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ToolLinkNodeProvider
{
	public class ToolLinkNodeProvider : IAmATreeNodeExtensionProvider
	{
		private readonly IToolLinkContext toolLinkContext;

		public ToolLinkNodeProvider(IToolLinkContext toolLinkContext)
		{
			this.toolLinkContext = toolLinkContext;
		}

		public IQueryable<IAmATreeNodeExtension> GetAll()
		{
			return toolLinkContext.GetAllToolLinks().AsQueryable();
		}

		public string Name
		{
			get { return "Tool Link"; }
		}

		public string ControllerToUseForModification
		{
			get { return "ToolLinkProviderNode"; }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForModification
		{
			get { return "Modify"; }
			set { throw new NotImplementedException(); }
		}

		public string ControllerToUseForCreation
		{
			get { return "ToolLinkProviderNode"; }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForCreation
		{
			get { return "Create"; }
			set { throw new NotImplementedException(); }
		}

		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public bool MayHaveChildNodes
		{
			get { return false; }
			set { throw new NotImplementedException(); }
		}
	}
}