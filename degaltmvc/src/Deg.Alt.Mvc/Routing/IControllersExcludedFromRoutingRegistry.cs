using System.Collections.Generic;

namespace Deg.Alt.Mvc.Routing
{
    public interface IControllersExcludedFromRoutingRegistry
    {
        IEnumerable<string> GetExcludedControllers();
    }
}