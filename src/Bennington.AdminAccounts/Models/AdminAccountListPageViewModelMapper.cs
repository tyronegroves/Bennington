using System.Collections.Generic;
using Bennington.AdminAccounts.Mappers;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountListPageViewModelMapper : IAdminAccountListPageViewModelMapper
    {
        public IEnumerable<AdminAccountListPageItem> CreateSet(IEnumerable<AdminAccount> adminAccounts)
        {
            var list = new List<AdminAccountListPageItem>();
            foreach (var adminAccount in adminAccounts)
                list.Add(new AdminAccountListPageItem
                             {
                                 FirstName = adminAccount.FirstName,
                                 Id = adminAccount.Id.ToString(),
                                 LastName = adminAccount.LastName,
                                 Username = adminAccount.Username
                             });
            return list;
        }
    }
}