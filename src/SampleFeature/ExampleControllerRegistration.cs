using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;

namespace SampleFeature
{
    public class ExampleControllerRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IAmATreeNodeExtensionProvider, ExampleController>(Guid.NewGuid().ToString());
        }
    }
}
