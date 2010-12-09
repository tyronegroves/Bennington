using System.Web.Mvc;
using MvcTurbine;
using MvcTurbine.Blades;
using Paragon.Pages.Binders;
using Paragon.Pages.Content;

namespace Paragon.Pages.Blades
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