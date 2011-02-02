using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.ComponentModel;
using MvcTurbine.Routing;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Routing;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Helpers;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ContentNodeProvider.Controllers
{
	public abstract class EngineController : Controller, IAmATreeNodeExtensionProvider, IRouteRegistrator, IRouteConstraint
    {
		private readonly IContentTreeNodeVersionContext contentTreeNodeVersionContext;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;

		public EngineController()
		{
			this.contentTreeNodeVersionContext = ServiceLocatorManager.Current.Resolve<IContentTreeNodeVersionContext>();
			this.treeNodeSummaryContext = ServiceLocatorManager.Current.Resolve<ITreeNodeSummaryContext>();
			this.treeNodeRepository = ServiceLocatorManager.Current.Resolve<ITreeNodeRepository>();
			this.treeNodeIdToUrl = ServiceLocatorManager.Current.Resolve<ITreeNodeIdToUrl>();
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

		//public void Register(RouteCollection routes)
		//{
		//    foreach (var treeNode in treeNodeRepository.GetAll().Where(a => a.Type == this.GetType().AssemblyQualifiedName))
		//    {
		//        var url = treeNodeIdToUrl.GetUrlByTreeNodeId(treeNode.Id);
		//        if (url.StartsWith("/")) url = url.Substring(1);
		//        url = url + "/{action}";
				
		//        var controllerName = (this.GetType().Name ?? string.Empty).Replace("Controller", string.Empty);

		//        routes.MapRoute(
		//            null,
		//            url,
		//            new { controller = controllerName, action = "Index" }
		//        );
		//    }
		//}
		public void Register(RouteCollection routes)
		{
			// add catch-all route 
			var contentTreeRoute = new Route
						(
							GetUrlPatternForDepth(ContentTreeRouteRegistrator.MaxDepthForContentTreeUrlSegments),
							GetDefaultRouteValues(ContentTreeRouteRegistrator.MaxDepthForContentTreeUrlSegments),
							new MvcRouteHandler()
						);
			contentTreeRoute.Constraints = new RouteValueDictionary();
			contentTreeRoute.Constraints.Add(GetType().AssemblyQualifiedName ?? "Unkown content tree route contraint", this);
			routes.Add(contentTreeRoute);

			// add hard coded routes for all instances of this engine type
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

		private static RouteValueDictionary GetDefaultRouteValues(int maxDepth)
		{
			var defaults = new RouteValueDictionary();

			for (var i = 0; i < maxDepth; i++)
				defaults.Add(string.Format("nodesegment-{0}", i), UrlParameter.Optional);

			defaults.Add("Controller", "ParagonPage");
			defaults.Add("Action", "Index");

			return defaults;
		}

		private static string GetUrlPatternForDepth(int maxDepth)
		{
			var builder = new StringBuilder("{nodesegment-0}");

			for (var i = 1; i < maxDepth; i++)
				builder.AppendFormat("/{{nodesegment-{0}}}", i);

			return builder.ToString();
		}

		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			var urlSegments = from value in values
							  where value.Key.StartsWith("nodesegment")
							  where value.Value is string
							  orderby value.Key.Split('-')[1]
							  select (string)value.Value;

			if (urlSegments.Count() == 0) return false;

			var treeNodeSummary = new TreeNodeSummary()
			{
				Id = Constants.RootNodeId,
			};
			foreach (var urlSegment in urlSegments)
			{
				treeNodeSummary = FindByUrlSegment(urlSegment, treeNodeSummary.Id);
				if (treeNodeSummary == null) return false;
			}

			return true;
		}

		private TreeNodeSummary FindByUrlSegment(string urlSegment, string parentTreeNodeId)
		{
			var children = treeNodeSummaryContext.GetChildren(parentTreeNodeId).Where(a => a.Type == GetType().AssemblyQualifiedName);
			return children.Where(a => a.UrlSegment == urlSegment).FirstOrDefault();
		}
    }
}
