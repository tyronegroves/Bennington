using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.ContentTree.SectionNodeProvider.Routing
{
    public class ContentTreeSectionRouteRegistrator //: IRouteRegistrator
    {
		public const int MaxDepthForContentTreeUrlSegments = 25;
    	private readonly ContentTreeSectionRouteConstraint contentTreeRouteConstraint;

    	public ContentTreeSectionRouteRegistrator(ContentTreeSectionRouteConstraint contentTreeRouteConstraint)
		{
    		this.contentTreeRouteConstraint = contentTreeRouteConstraint;
		}

    	public void Register(RouteCollection routes)
        {
			var contentTreeRoute = new Route
						(
							GetUrlPatternForDepth(MaxDepthForContentTreeUrlSegments),
							GetDefaultRouteValues(MaxDepthForContentTreeUrlSegments),
							new MvcRouteHandler()
						);
			contentTreeRoute.Constraints = new RouteValueDictionary();
			contentTreeRoute.Constraints.Add(contentTreeRouteConstraint.GetType().AssemblyQualifiedName ?? "Unkown content tree route contraint", contentTreeRouteConstraint);
			routes.Add(contentTreeRoute);
        }

		private static RouteValueDictionary GetDefaultRouteValues(int maxDepth)
		{
			var defaults = new RouteValueDictionary();

			for (var i = 0; i < maxDepth; i++)
				defaults.Add(string.Format("nodesegment-{0}", i), UrlParameter.Optional);

			defaults.Add("Controller", "ContentTreeSection");
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
    }
}