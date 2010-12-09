using System.Linq;
using Deg.Alt.Mvc.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Helpers
{
    [TestClass]
    public class EmptyControllersExcludedFromRoutingRegistryTests
    {
        [TestMethod]
        public void Returns_an_empty_list()
        {
            var registry = new EmptyControllersExcludedFromRoutingRegistry();
            var results = registry.GetExcludedControllers();

            Assert.AreEqual(0, results.Count());
        }
    }
}