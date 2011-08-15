using System.Collections.Generic;
using Bennington.AdminAccounts.Models;

namespace Bennington.AdminAccounts.Mappers
{
    public interface IAdminAccountListPageViewModelMapper
    {
        IEnumerable<AdminAccountListPageItem> CreateSet(IEnumerable<AdminAccount> adminAccounts);
    }
}