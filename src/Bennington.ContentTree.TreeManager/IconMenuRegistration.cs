using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;

namespace Bennington.ContentTree.TreeManager
{
    public class MenuSystemConfigurer : IMenuSystemConfigurer
    {
        public void Configure(IMenuRegistry sectionMenuRegistry)
        {
            sectionMenuRegistry.Add(new ActionIconMenuItem("Content Tree", "Content/Canvas/ContentTreeManagementIcon.gif", "Index", "TreeManager"));
        }
    }
}
