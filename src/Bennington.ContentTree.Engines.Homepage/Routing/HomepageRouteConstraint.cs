using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Engines.Homepage.Controllers;

namespace Paragon.ContentTree.Engines.Homepage.Routing
{
	public class HomepageRouteConstraint : IRouteConstraint
	{
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public HomepageRouteConstraint(ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			var homepage = treeNodeSummaryContext.GetChildren(Constants.RootNodeId).Where(a => a.Type == typeof(HomepageController).AssemblyQualifiedName).FirstOrDefault();

			if ((homepage!= null) && (httpContext.Request.RawUrl == "/"))
				return true;

			return false;
		}
	}
}
