using AutoMapperAssist;
using Bennington.AdminAccounts.Controllers;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountListPageViewModelMapper : Mapper<AdminAccount, AdminAccountListPageViewModel>,
                                                       IAdminAccountListPageViewModelMapper
    {
    }
}