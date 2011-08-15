using System.Collections.Generic;
using System.Web.Routing;
using Bennington.Cms.Buttons;
using Bennington.Cms.Models;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountListPageViewModel : ListPageViewModel<AdminAccountListPageItem>
    {
    }

    public class AdminAccountListPageViewModelRegistry : IListPageButtonRegistry<AdminAccountListPageItem>
    {
        public IEnumerable<Button> GetTheTopRightButtons()
        {
            return new Button[] {};
        }

        public IEnumerable<Button> GetTheBottomRightButtons()
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["controller"] = "AdminAccount";
            routeValueDictionary["action"] = "Edit";
            return new[]
                       {
                           new
                               RoutesButton
                               {
                                   Id = "CreateAdminAccount",
                                   Text = "Create",
                                   RouteValues = routeValueDictionary
                               }
                       }
                ;
        }
    }
}