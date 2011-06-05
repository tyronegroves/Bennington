using System;
using System.Collections.Generic;
using System.Linq;
using Bennington.Cms.Models;
using MvcTurbine;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Blades
{
    public class SubMenuItemRegistryBlade : Blade, ISupportAutoRegistration
    {
        public override void Spin(IRotorContext context)
        {
        }

        public void AddRegistrations(AutoRegistrationList registrationList)
        {
            registrationList.Add(MvcTurbine.ComponentModel.Registration.Simple<ISubMenuItemRegistry>());
        }
    }

    public class SectionMenuRegistrationBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;

            var list = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var type in assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof (ISectionMenuItemRegistry)))
                    .Where(x => x.IsAbstract == false && x.IsInterface == false))
                    list.Add(type);

            var first = list.OrderBy(x => x.Assembly == typeof (ISectionMenuItemRegistry).Assembly ? 1 : 0)
                .FirstOrDefault();

            if (first != null)
                serviceLocator.Register<ISectionMenuItemRegistry>(first);
        }
    }
}