using System.Web.Routing;
using Deg.Alt.Mvc.Mappers;

namespace Deg.Alt.Mvc.Routing.InstinctRouteHandlers
{
    public interface IInstinctRouteHttpHandler
    {
        string CalculateTheControllerName(RouteValueDictionary dictionary);
    }

    public class InstinctRouteHttpHandler : InstinctRouteHttpHandlerBase, IInstinctRouteHttpHandler
    {
        private readonly IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper;

        public InstinctRouteHttpHandler(IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper)
        {
            this.routeValueDictionaryToPageLocationMapper = routeValueDictionaryToPageLocationMapper;
        }

        public override string CalculateTheControllerName(RouteValueDictionary dictionary)
        {
            var pageLocation = routeValueDictionaryToPageLocationMapper.CreateInstance(dictionary);
            return pageLocation.Controller;
        }
    }
}