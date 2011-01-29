using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.ComponentModel;
using MvcTurbine.Routing;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.Helpers;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ContentNodeProvider.Controllers
{
	public abstract class EngineController : Controller, IAmATreeNodeExtensionProvider, IRouteRegistrator
    {
		private readonly IContentTreeNodeVersionContext contentTreeNodeVersionContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;

		public EngineController()
		{
			this.treeNodeIdToUrl = ServiceLocatorManager.Current.Resolve<ITreeNodeIdToUrl>();
			this.treeNodeRepository = ServiceLocatorManager.Current.Resolve<ITreeNodeRepository>();
			this.contentTreeNodeVersionContext = ServiceLocatorManager.Current.Resolve<IContentTreeNodeVersionContext>();
		}

		public virtual IQueryable<IAmATreeNodeExtension> GetAll()
		{
			var query = from item in contentTreeNodeVersionContext.GetAllContentTreeNodes().Where(a => a.Action == "Index")
						select item;

			return query;
		}

		public abstract string Name {get;}

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

		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get
			{
				var contentTreeNodeContentItems = new List<ContentTreeNodeContentItem>();
				foreach (var method in this.GetType().GetMethods().Where(a => a.IsPublic && ((Type)a.ReturnParameter.ParameterType == typeof(ActionResult))))
				{
					if (method.DeclaringType == this.GetType())
					{
						contentTreeNodeContentItems.Add(new ContentTreeNodeContentItem()
						{
							Id = method.Name,
							Name = method.Name,
						});

					}
				}
				return contentTreeNodeContentItems;
			}
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

		public void Register(RouteCollection routes)
		{
			foreach (var treeNode in treeNodeRepository.GetAll().Where(a => a.Type == this.GetType().AssemblyQualifiedName))
			{
				var url = treeNodeIdToUrl.GetUrlByTreeNodeId(treeNode.Id);
				if (url.StartsWith("/")) url = url.Substring(1);
				url = url + "/{action}";
				
				var controllerName = (this.GetType().Name ?? string.Empty).Replace("Controller", string.Empty);

				routes.MapRoute(
					null,
					url,
					new { controller = controllerName, action = "Index" }
				);
			}
		}
    }
}
