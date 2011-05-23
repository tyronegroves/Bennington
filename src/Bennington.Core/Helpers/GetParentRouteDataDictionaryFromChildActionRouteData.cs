//using System.Web.Mvc;
//using System.Web.Routing;

//namespace Bennington.Core.Helpers
//{
//    ///<summary>
//    ///</summary>
//    public interface IGetParentRouteDataDictionaryFromChildActionRouteData
//    {
//        ///<summary>
//        ///</summary>
//        ///<param name="childActionRouteData"></param>
//        ///<returns></returns>
//        RouteData GetRouteValues(RouteData childActionRouteData);
//    }

//    ///<summary>
//    ///</summary>
//    public class GetParentRouteDataDictionaryFromChildActionRouteData : IGetParentRouteDataDictionaryFromChildActionRouteData
//    {
//        public RouteData GetRouteValues(RouteData childActionRouteData)
//        {
//            if (childActionRouteData == null) return null;
//            if (childActionRouteData.DataTokens != null)
//            {
//                var parentActionViewContext = ((ViewContext)childActionRouteData.DataTokens["ParentActionViewContext"]);
//                if (parentActionViewContext != null)
//                {
//                    return parentActionViewContext.RouteData;
//                }
//            }
//            return null;
//        }
//    }

//}
