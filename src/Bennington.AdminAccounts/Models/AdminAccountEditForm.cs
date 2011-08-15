using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using Bennington.Cms.Models;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountEditForm : EditForm
    {
        [Hidden]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AdminAccountEditFormButtons : IEditPageButtonRegistry<AdminAccountEditForm>
    {
        public IEnumerable<Button> GetTheActionButtons(AdminAccountEditForm adminAccountEditForm)
        {
            return new Button[]
                       {
                           new SubmitButton {Text = "Save"},
                           new SubmitButton {Id = "SaveAndExit", Text = "Save and Exit"},
                           new RoutesButton {Text = "Cancel", RouteValues = GetCancelButtonRouteDictionary()},
                           new RoutesButton {Id = "DeleteButton", Text = "Delete", RouteValues = GetDeleteButtonRouteDictionary(adminAccountEditForm)}
                       };
        }

        private static RouteValueDictionary GetDeleteButtonRouteDictionary(AdminAccountEditForm adminAccountEditForm)
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["controller"] = "AdminAccount";
            routeValueDictionary["action"] = "Delete";
            routeValueDictionary["id"] = adminAccountEditForm.Id;
            return routeValueDictionary;
        }

        private static RouteValueDictionary GetCancelButtonRouteDictionary()
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["controller"] = "AdminAccount";
            routeValueDictionary["action"] = "Index";
            return routeValueDictionary;
        }
    }
}