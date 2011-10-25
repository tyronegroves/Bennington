using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Routing.Routing;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Repositories;
using Bennington.ContentTree.Repositories;
using Bennington.Core.Helpers;
using MvcTurbine.Routing;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Controllers
{
    public class ToolLinkController : Controller, IRouteRegistrator, IRouteConstraint
    {
        private readonly ITreeNodeRepository treeNodeRepository;
        private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
        private readonly IUrlToTreeNodeSummaryMapper urlToTreeNodeSummaryMapper;
        private readonly IRawUrlGetter rawUrlGetter;
        private readonly IToolLinkProviderDraftRepository toolLinkProviderDraftRepository;

        public ToolLinkController(ITreeNodeRepository treeNodeRepository, 
                                    ITreeNodeIdToUrl treeNodeIdToUrl,
                                    IUrlToTreeNodeSummaryMapper urlToTreeNodeSummaryMapper,
                                    IRawUrlGetter rawUrlGetter,
                                    IToolLinkProviderDraftRepository toolLinkProviderDraftRepository)
        {
            this.toolLinkProviderDraftRepository = toolLinkProviderDraftRepository;
            this.rawUrlGetter = rawUrlGetter;
            this.urlToTreeNodeSummaryMapper = urlToTreeNodeSummaryMapper;
            this.treeNodeIdToUrl = treeNodeIdToUrl;
            this.treeNodeRepository = treeNodeRepository;
        }

        public ActionResult Index()
        {
            var url = rawUrlGetter.GetRawUrl();
            var treeNodeSummary = urlToTreeNodeSummaryMapper.CreateInstance(url);

            var toolLink = toolLinkProviderDraftRepository.GetAll().Where(a => a.Id == treeNodeSummary.Id).FirstOrDefault();

            if (toolLink == null) return new HttpNotFoundResult();

            return new RedirectResult(toolLink.Url);
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

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration) return false;

            var url = rawUrlGetter.GetRawUrl();

            var treeNodeSummary = urlToTreeNodeSummaryMapper.CreateInstance(url);

            if (treeNodeSummary == null) return false;

            // if the last nodesegment doesn't match treeNodeSummary.UrlSegment then return false
            if (FindLastNodeSegmentInRouteData(values) != treeNodeSummary.UrlSegment) return false;

            var match = (typeof(ToolLinkNodeProvider).AssemblyQualifiedName.StartsWith(treeNodeSummary.Type));
            return match;
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

    }
}
