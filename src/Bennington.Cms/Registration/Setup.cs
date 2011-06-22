using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTurbine;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;
using MvcTurbine.Web.Blades;
using MvcTurbine.Web.Config;

namespace Bennington.Cms.Registration
{
    public class Setup : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            CoreBlades.UnTrack<ViewBlade>();
            CoreBlades.Track<SpecialViewBlade>();
        }
    }

    public class SpecialViewBlade : CoreBlade    {
        /// <summary>
        /// Initializes the <see cref="ViewEngines.Engines"/> by pulling all associated <seealso cref="IViewEngine"/> instances
        /// in the current application.
        /// </summary>
        /// <param name="context"></param>
        public override void Spin(IRotorContext context)
        {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            // Clear all ViewEngines
            //ViewEngines.Engines.Clear();


            // Re-add the WebForms view engine since that's the default one
            //ViewEngines.Engines.Add(new WebFormViewEngine());
        }
    }
}