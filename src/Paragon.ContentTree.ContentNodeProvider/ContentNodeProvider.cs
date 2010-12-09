using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ContentNodeProvider
{
	public class ContentNodeProvider : IAmATreeNodeExtensionProvider
	{
		private readonly IContentTreeNodeRepository contentTreeNodeRepository;

		public ContentNodeProvider(IContentTreeNodeRepository contentTreeNodeRepository)
		{
			this.contentTreeNodeRepository = contentTreeNodeRepository;
		}

		public virtual IQueryable<IAmATreeNodeExtension> GetAll()
		{
			var query = from item in contentTreeNodeRepository.GetAllContentTreeNodes().Where(a => a.ContentItemId == "Index")
						select item;
			
			return query;
		}

		public virtual string Name
		{
			get { return "Page"; }
		}

		public virtual string ControllerToUseForCreation
		{
			get { return ControllerToUseForModification; }
			set { throw new NotImplementedException(); }
		}

		public virtual string ActionToUseForCreation
		{
			get { return "Create"; }
			set { throw new NotImplementedException(); }
		}

		public virtual IRouteConstraint IgnoreConstraint
		{
			get { return null; }
		}

		public virtual IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get { return new ContentTreeNodeContentItem[]
			             	{
			             		new ContentTreeNodeContentItem()
			             			{
			             				Id = "Index",
										Name = "Page Content",
			             			}, 
							}; }
			set { throw new NotImplementedException(); }
		}

		public virtual string ControllerToUseForModification
		{
			get { return "ContentTreeNode"; }
			set { throw new NotImplementedException(); }
		}

		public virtual string ActionToUseForModification
		{
			get { return "Modify"; }
			set { throw new NotImplementedException(); }
		}
	}
}