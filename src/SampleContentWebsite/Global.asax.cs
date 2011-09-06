using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;

namespace SampleContentWebsite
{
    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}