using System.Web.Mvc;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public interface ISectionMenuItem
    {
        SectionMenuItemViewModel GetViewModel(ControllerContext controllerContext);
    }
}