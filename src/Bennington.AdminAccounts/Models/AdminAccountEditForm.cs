using System.ComponentModel.DataAnnotations;
using Bennington.Core.List;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountEditForm
    {
        [Hidden]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}