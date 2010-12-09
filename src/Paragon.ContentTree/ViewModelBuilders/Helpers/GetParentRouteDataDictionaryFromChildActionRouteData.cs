using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paragon.ContentTree.ViewModelBuilders.Helpers
{
	public interface IGetParentRouteDataDictionaryFromChildActionRouteData
	{
		RouteData GetRouteValues(RouteData childActionRouteData);
	}

	public class GetParentRouteDataDictionaryFromChildActionRouteData : IGetParentRouteDataDictionaryFromChildActionRouteData
	{
		public RouteData GetRouteValues(RouteData childActionRouteData)
		{
			if (childActionRouteData == null) return null;
			if (childActionRouteData.DataTokens != null)
			{
				var parentActionViewContext = ((ViewContext)childActionRouteData.DataTokens["ParentActionViewContext"]);
				if (parentActionViewContext != null)
				{
					return parentActionViewContext.RouteData;
				}
			}
			//var action = ((ViewContext)childActionRouteData.DataTokens["ParentActionViewContext"]).RouteData.Values["action"];
			return null;
		}
	}
}