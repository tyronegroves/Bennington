using System.Linq;
using System.Web;
using System.Web.Routing;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Routing.RouteCreators;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Routing.RouteConstraints
{
    public class SectionRouteConstraint : ISectionRouteConstraint
    {
        private readonly IServiceLocator serviceLocator;

        public SectionRouteConstraint(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var sectionId = GetTheSectionId(values);
            return serviceLocator.Resolve<ISectionRepository>().GetSections()
                .Any(x => string.Compare(x.Id, sectionId, true) == 0);
        }

        private string GetTheSectionId(RouteValueDictionary values)
        {
            return (string)values["sectionId"];
        }
    }
}