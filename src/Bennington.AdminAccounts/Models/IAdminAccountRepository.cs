using System.Collections.Generic;

namespace Bennington.AdminAccounts.Models
{
    public interface IAdminAccountRepository
    {
        IEnumerable<AdminAccount> GetAllAdminAccounts();
    }
}