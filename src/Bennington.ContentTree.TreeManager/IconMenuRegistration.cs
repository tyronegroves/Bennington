using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;

namespace Bennington.ContentTree.TreeManager
{
    public class MenuSystemConfigurer : IMenuSystemConfigurer, IIconMenuItem
    {
        public void Configure(IMenuRegistry sectionMenuRegistry)
        {
            sectionMenuRegistry.Add(this);
        }

        public IconMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new IconMenuItemViewModel()
                       {
                           Name = "Content Tree",
                           IconUrl = "/Content/Canvas/ContentTreeManagementIcon.gif",
                           //Url = 
                       };
        }
    }
}
