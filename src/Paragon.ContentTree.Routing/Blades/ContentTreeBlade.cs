using System.Web.Mvc;
using MvcTurbine;
using MvcTurbine.Blades;
using Paragon.ContentTree.Routing.Binders;

namespace Paragon.ContentTree.Routing.Blades
{
    public class ContentTreeBlade : IBlade
    {
        public void Spin(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            var modelBinders = ModelBinders.Binders;
			modelBinders.Add(typeof(Content.ContentTree), serviceLocator.Resolve<ContentTreeModelBinder>());
        }

        public void Initialize(IRotorContext context)
        {
        }

        public void Dispose()
        {
        }
    }
}