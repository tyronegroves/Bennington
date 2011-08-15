using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public IEnumerable<Button> GetTheActionButtons(AdminAccountEditForm @object)
        {
            return new[]
                       {
                           new SubmitButton{Text = "Save"}
                       };
        }
    }
}