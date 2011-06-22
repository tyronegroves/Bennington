using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.AdminAccounts.Models;
using Bennington.Cms.Models;

namespace Bennington.AdminAccounts.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly IAdminAccountRepository adminAccountRepository;
        private readonly IAdminAccountListPageViewModelMapper adminAccountListPageViewModelMapper;

        public AdminAccountController(IAdminAccountRepository adminAccountRepository,
                                      IAdminAccountListPageViewModelMapper adminAccountListPageViewModelMapper)
        {
            this.adminAccountRepository = adminAccountRepository;
            this.adminAccountListPageViewModelMapper = adminAccountListPageViewModelMapper;
        }

        public ActionResult Index()
        {
            var adminAccounts = adminAccountRepository.GetAllAdminAccounts();
    
            var mappedResults = adminAccountListPageViewModelMapper.CreateSet(adminAccounts);

            return View("Index", new ListPageViewModel<AdminAccountListPageViewModel>{Items = mappedResults.AsQueryable()});
        }
    }

    public interface IAdminAccountListPageViewModelMapper
    {
        IEnumerable<AdminAccountListPageViewModel> CreateSet(IEnumerable<AdminAccount> adminAccounts);
    }

    public interface IAdminAccountRepository
    {
        IEnumerable<AdminAccount> GetAllAdminAccounts();
    }
}