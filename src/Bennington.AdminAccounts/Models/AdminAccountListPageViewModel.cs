﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Routing;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using MvcTurbine.Web.Metadata;

namespace Bennington.AdminAccounts.Models
{
    [LoadButtonsForTheAdminPage]
    public class AdminAccountListPageViewModel
    {
        [DoNotShowThisProperty]
        public string Id { get; set; }

        public string Username { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }

    public class LoadButtonsForTheAdminPage : MetadataAttribute
    {
    }

    public class LoadButtonsForTheAdminPageHandler : LoadTheseButtonsForEachRow<AdminAccountListPageViewModel>, IMetadataAttributeHandler<LoadButtonsForTheAdminPage>
    {
        public override IEnumerable<Button> GetButtons(AdminAccountListPageViewModel model)
        {
            if (model == null) return new Button[] { };
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["action"] = "Edit";
            routeValueDictionary["controller"] = "AdminAccount";
            routeValueDictionary["id"] = model.Id;

            return new[]
                       {
                           new RoutesButton {Id = "Edit", Text = "Edit", RouteValues = routeValueDictionary}
                       };
        }
    }

}