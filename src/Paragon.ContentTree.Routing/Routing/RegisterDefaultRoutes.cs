using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Routing.Content;

namespace Paragon.ContentTree.Routing.Routing
{
    public class RegisterDefaultRoutes : IRouteRegistrator
    {
    	private readonly IContentTreeBuilder contentTreeBuilder;
    	private ITreeNodeProviderContext treeNodeProviderContext;
    	private ITreeNodeSummaryContext treeNodeSummaryContext;

    	public RegisterDefaultRoutes(IContentTreeBuilder contentTreeBuilder, 
									ITreeNodeProviderContext treeNodeProviderContext,
									ITreeNodeSummaryContext treeNodeSummaryContext)
		{
    		this.treeNodeSummaryContext = treeNodeSummaryContext;
    		this.treeNodeProviderContext = treeNodeProviderContext;
    		this.contentTreeBuilder = contentTreeBuilder;
		}

    	public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			var contentTree = contentTreeBuilder.GetContentTree();
    		var contentTreeRouteBuilder = new ContentTreeRouteBuilder(treeNodeSummaryContext);
			contentTreeRouteBuilder.MapRoutes(contentTree);
        }
    }
}