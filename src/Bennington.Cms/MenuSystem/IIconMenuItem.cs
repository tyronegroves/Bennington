using System.Web.Mvc;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public interface IIconMenuItem
    {
        IconMenuItemViewModel GetViewModel(ControllerContext controllerContext);
    }
}