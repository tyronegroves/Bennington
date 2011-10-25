using System.ComponentModel;
using Bennington.Cms.Attributes;
using Bennington.Core.List;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountListPageItem
    {
        [Hidden]
        public string Id { get; set; }

        public string Username { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}