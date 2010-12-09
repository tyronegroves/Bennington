using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.SectionNodeProvider.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.SectionNodeProvider
{
	public class SectionNodeProvider : IAmATreeNodeExtensionProvider
	{
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;

		public SectionNodeProvider(IContentTreeSectionNodeRepository contentTreeSectionNodeRepository)
		{
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
		}

		public IQueryable<IAmATreeNodeExtension> GetAll()
		{
			var query = from item in contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes()
						select item;
			
			return query;
		}

		public string Name
		{
			get { return "Section"; }
		}

		public string ControllerToUseForCreation
		{
			get { return ControllerToUseForModification; }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForCreation
		{
			get { return "Create"; }
			set { throw new NotImplementedException(); }
		}

		public IRouteConstraint IgnoreConstraint
		{
			get { return null; }
		}

		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ControllerToUseForModification
		{
			get { return "ContentTreeSectionNode"; }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForModification
		{
			get { return "Modify"; }
			set { throw new NotImplementedException(); }
		}
	}
}