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

		public RegisterDefaultRoutes(IContentTreeBuilder contentTreeBuilder, ITreeNodeProviderContext treeNodeProviderContext)
		{
			this.treeNodeProviderContext = treeNodeProviderContext;
    		this.contentTreeBuilder = contentTreeBuilder;
		}

    	public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			var contentTree = contentTreeBuilder.GetContentTree();
    		var contentTreeRouteBuilder = new ContentTreeRouteBuilder();
			contentTreeRouteBuilder.MapRoutes(contentTree);
        }
    }
}