using System.Linq;
using System.Web;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Routing
{
    public class ContentTreeRouteConstraint : IRouteConstraint
    {
    	private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

    	public ContentTreeRouteConstraint(ITreeNodeSummaryContext treeNodeSummaryContext)
    	{
    		this.treeNodeSummaryContext = treeNodeSummaryContext;
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
			var children = treeNodeSummaryContext.GetChildren(parentTreeNodeId).Where(a => a.MayHaveChildNodes); //.Where(a => a.Type == typeof(ContentNodeProvider).AssemblyQualifiedName);)
			return children.Where(a => a.UrlSegment == urlSegment).FirstOrDefault();
		}
    }
}