using System;
using System.Collections.Generic;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;

namespace SampleApp
{
    //public class TestingRegistry : ISectionMenuItemRegistry
    //{
    //    public IEnumerable<SectionMenuItem> GetItems()
    //    {
    //        return new[] { 
    //            new SectionMenuItemForAUrl
    //            {
    //                Name = "Button!",
    //                Url = "/MANAGE/BUTTON"
    //            }
    //        };
    //    }
    //}

    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}