using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.ContentTree.Contexts;
using Paragon.Pages.Content;

namespace Paragon.Pages.Routing
{
    public class ContentTreeRouteConstraint : IRouteConstraint
    {
		private readonly Paragon.Pages.Content.ContentTree contentTree;
		//private readonly ITreeNodeProviderContext treeNodeProviderContext;

		public ContentTreeRouteConstraint(Content.ContentTree contentTree/*, ITreeNodeProviderContext treeNodeProviderContext*/)
		{
			//this.treeNodeProviderContext = treeNodeProviderContext;
			this.contentTree = contentTree;
		}

    	public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
			//foreach (var treeNodeProvider in treeNodeProviderContext.GetAllTreeNodeProviders())
			//{
			//    if (treeNodeProvider.IgnoreConstraint == null) continue;
			//    if (treeNodeProvider.IgnoreConstraint.Match(httpContext, route, parameterName, values, routeDirection))
			//    {
			//        return false;
			//    }
			//}
			
			var segments = from value in values
                           where value.Key.StartsWith("nodesegment")
                           where value.Value is string
                           orderby value.Key.Split('-')[1]
                           select (string)value.Value;

            if (segments.Count() == 0) return false;

            var rootNode = contentTree.RootNode;
            var childNodes = rootNode.ChildNodes;

            foreach (var currentNode in segments.Select(segment => childNodes.FindByUrlSegment(segment)))
            {
                if (currentNode == null) return false;
                childNodes = currentNode.ChildNodes;
            }

            return true;
        }
    }
}