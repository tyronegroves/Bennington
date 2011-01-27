using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.ContentTree.Routing.Routing
{
	public interface IContentTreeRouteBuilder
	{
		void MapRoutes(Content.ContentTree contentTree);
	}

	public class ContentTreeRouteBuilder : IContentTreeRouteBuilder
	{
		private ITreeNodeSummaryContext treeNodeSummaryContext;

		public ContentTreeRouteBuilder(ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public void MapRoutes(Content.ContentTree contentTree)
        {
			var maxDepth = 20; // contentTree.MaxDepth;
            var urlPattern = GetUrlPatternForDepth(maxDepth);
            var defaults = GetRouteDefaults(maxDepth);
            var constraints = GetRouteConstraints(contentTree);
            
            var contentTreeRoute = new ContentTreeRoute(urlPattern, defaults, constraints);

            var routes = RouteTable.Routes;
            var rootNode = contentTree.RootNode;
			
			var nodesWantingCustomRouting = GetAllContentTreeNodes(rootNode).OfType<IWantCustomRouting>();

            using (routes.GetWriteLock())
            {
                var route = routes.OfType<ContentTreeRoute>().FirstOrDefault();
                if (route != null)
                    routes.Remove(route);

                nodesWantingCustomRouting.ForEach(node => node.RemoveRoute(routes));
				nodesWantingCustomRouting.ForEach(node => routes.Add(node.GetCustomRoute()));

				routes.Add(contentTreeRoute);
            }
        }

		private RouteValueDictionary GetRouteConstraints(Content.ContentTree contentTree)
        {
			return new RouteValueDictionary(new { nodeSegmentConstraint = new ContentTreeRouteConstraint(treeNodeSummaryContext) });
        }

        private static RouteValueDictionary GetRouteDefaults(int maxDepth)
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

        private static IEnumerable<ContentTreeNode> GetAllContentTreeNodes(ContentTreeNode node)
        {
            var nodes = new List<ContentTreeNode> {node};
            
            foreach(var childNode in node)
                nodes.AddRange(GetAllContentTreeNodes(childNode));

            return nodes;
        }
    }
}