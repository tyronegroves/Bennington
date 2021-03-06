﻿using System.Web.Mvc;
using Bennington.Cms.Models.MenuSystem;

namespace Bennington.Cms.MenuSystem
{
    public interface IIconMenuItem
    {
        IconMenuItemViewModel GetViewModel(ControllerContext controllerContext);
    }
}