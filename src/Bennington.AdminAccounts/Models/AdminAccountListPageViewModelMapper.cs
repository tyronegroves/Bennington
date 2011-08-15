using AutoMapperAssist;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Mappers;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountListPageViewModelMapper : Mapper<AdminAccount, AdminAccountListPageItem>,
                                                       IAdminAccountListPageViewModelMapper
    {
    }
}