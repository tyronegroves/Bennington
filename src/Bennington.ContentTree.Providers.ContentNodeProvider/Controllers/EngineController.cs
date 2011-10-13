using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.Context;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.Routing;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders.Helpers;
using Bennington.ContentTree.Repositories;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;
using MvcTurbine.Routing;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Controllers
{
	public abstract class EngineController : Controller, IAmATreeNodeExtensionProvider, IRouteRegistrator, IRouteConstraint
    {
		private readonly IContentTreeNodeVersionContext contentTreeNodeVersionContext;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly IUrlToTreeNodeSummaryMapper urlToTreeNodeSummaryMapper;
		private readonly IRawUrlGetter rawUrlGetter;

		public EngineController()
		{
			this.contentTreeNodeVersionContext = ServiceLocatorManager.Current.Resolve<IContentTreeNodeVersionContext>();
			this.treeNodeSummaryContext = ServiceLocatorManager.Current.Resolve<ITreeNodeSummaryContext>();
			this.treeNodeRepository = ServiceLocatorManager.Current.Resolve<ITreeNodeRepository>();
			this.treeNodeIdToUrl = ServiceLocatorManager.Current.Resolve<ITreeNodeIdToUrl>();
			this.urlToTreeNodeSummaryMapper = ServiceLocatorManager.Current.Resolve<IUrlToTreeNodeSummaryMapper>();
			this.rawUrlGetter = ServiceLocatorManager.Current.Resolve<IRawUrlGetter>();
		}

		public virtual IQueryable<IAmATreeNodeExtension> GetAll()
		{
		    var query = from item in contentTreeNodeVersionContext.GetAllContentTreeNodes().Where(a => a.Action == "Index")
		                select item;

		    var treeNodeExtensions = new List<IAmATreeNodeExtension>();
            foreach (var item in query)
            {
                item.IconUrl = "Content/ContentNodeProvider/controller.gif";
                treeNodeExtensions.Add(item);
            }

			return treeNodeExtensions.AsQueryable();
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

		public bool MayHaveChildNodes
		{
			get { return false; }
			set { throw new NotImplementedException(); }
		}

		public void RegisterRouteForTreeNodeId(string treeNodeId)
		{
			var routeValueDictionary = new RouteValueDictionary();
			routeValueDictionary.Add("Controller", GetControllerNameFromThisType());
			routeValueDictionary.Add("Action", "Index");
			var url = treeNodeIdToUrl.GetUrlByTreeNodeId(treeNodeId);
			var contentTreeRoute = new Route
						(
							url.Substring(1, url.Length - 1) + "/{action}",
							routeValueDictionary,
							new MvcRouteHandler()
						);
			contentTreeRoute.Constraints = new RouteValueDictionary();
			contentTreeRoute.Constraints.Add(GetType().AssemblyQualifiedName ?? "Unkown content tree route contraint", this);
			RouteTable.Routes.Add(contentTreeRoute);
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
		    // add catch-all routes for incoming routes that will match dynamically created controllers
            for (var n = 0; n < 3 /*ContentTreeRouteRegistrator.MaxDepthForContentTreeUrlSegments*/; n++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x <= n; x++)
                {
                    sb.Append(string.Format("{{nodesegment-{0}}}/", x));
                }
                AddRoute(routes, string.Format("{0}{{action}}", sb));
            }

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

	    private void AddRoute(RouteCollection routes, string urlPattern)
	    {
	        var contentTreeRoute = new Route
	            (
	            urlPattern,
	            GetDefaultRouteValues(ContentTreeRouteRegistrator.MaxDepthForContentTreeUrlSegments),
	            new MvcRouteHandler()
	            );
	        contentTreeRoute.Constraints = new RouteValueDictionary();
	        contentTreeRoute.Constraints.Add(GetType().AssemblyQualifiedName ?? "Unkown content tree route contraint", this);
	        routes.Add(contentTreeRoute);
	    }

	    private RouteValueDictionary GetDefaultRouteValues(int maxDepth)
        {
            var defaults = new RouteValueDictionary();

            for (var i = 0; i < maxDepth; i++)
                defaults.Add(string.Format("nodesegment-{0}", i), UrlParameter.Optional);

            defaults.Add("Controller", GetControllerNameFromThisType());
            defaults.Add("Action", "Index");

            return defaults;
        }

		private string GetControllerNameFromThisType()
		{
			return GetType().Name.Replace("Controller", string.Empty);
		}

        private string GetUrlPatternForDepth(int maxDepth)
        {
            var builder = new StringBuilder("{nodesegment-0}/{action}");

            //for (var i = 1; i < maxDepth; i++)
            //    builder.AppendFormat("/{{nodesegment-{0}}}", i);

            return builder.ToString();
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration) return false;

            var treeNodeSummary = urlToTreeNodeSummaryMapper.CreateInstance(rawUrlGetter.GetRawUrl());

            if (treeNodeSummary == null) return false;

            // if the last nodesegment doesn't match treeNodeSummary.UrlSegment then return false
            if (FindLastNodeSegmentInRouteData(values) != treeNodeSummary.UrlSegment) return false;

            return (treeNodeSummary.Type == GetType().AssemblyQualifiedName);
        }

	    private string FindLastNodeSegmentInRouteData(RouteValueDictionary values)
	    {
	        int n;
            for (n = 0; n < ContentTreeRouteRegistrator.MaxDepthForContentTreeUrlSegments; n++)
            {
                var nextNodeSegmentRouteValueKey = string.Format("nodesegment-{0}", n);
                if (!values.ContainsKey(nextNodeSegmentRouteValueKey)) break;
                if (values[nextNodeSegmentRouteValueKey] == null) break;
                if (string.IsNullOrEmpty(values[nextNodeSegmentRouteValueKey].ToString())) break;
            }
            if (n == 0) return string.Empty;

	        return values[string.Format("nodesegment-{0}", n - 1)].ToString();
	    }

	    private TreeNodeSummary FindByUrlSegment(string urlSegment, string parentTreeNodeId)
        {
            var children = treeNodeSummaryContext.GetChildren(parentTreeNodeId).Where(a => a.Type == GetType().AssemblyQualifiedName);
            return children.Where(a => a.UrlSegment == urlSegment).FirstOrDefault();
        }
    }
}
