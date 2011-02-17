using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Models;
using Bennington.ContentTree.Providers.SectionNodeProvider.Repositories;
using Bennington.ContentTree.TreeNodeExtensionProvider;

namespace Bennington.ContentTree.Providers.SectionNodeProvider
{
	public class SectionNodeProvider : IAmATreeNodeExtensionProvider
	{
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;
		private IVersionContext versionContext;

		public SectionNodeProvider(IContentTreeSectionNodeRepository contentTreeSectionNodeRepository,
									IVersionContext versionContext)
		{
			this.versionContext = versionContext;
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
		}

		public IQueryable<IAmATreeNodeExtension> GetAll()
		{
			var query = from item in contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.Inactive == false || versionContext.GetCurrentVersionId() == VersionContext.Manage)
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

		public bool MayHaveChildNodes
		{
			get { return true; }
			set { throw new NotImplementedException(); }
		}

		public void RegisterRouteForTreeNodeId(string treeNodeId)
		{
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