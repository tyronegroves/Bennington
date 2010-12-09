using System.Collections.Generic;
using Deg.Alt.Mvc.Routing;

namespace Deg.Alt.Mvc.Helpers
{
    public class EmptyControllersExcludedFromRoutingRegistry : IControllersExcludedFromRoutingRegistry
    {
        public IEnumerable<string> GetExcludedControllers()
        {
            return new string[] {};
        }
    }
}