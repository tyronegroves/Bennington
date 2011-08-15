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
        private readonly IAdminAccountEditFormStore adminAccountEditFormStore;

        public AdminAccountController(IAdminAccountRepository adminAccountRepository,
                                      IAdminAccountListPageViewModelMapper adminAccountListPageViewModelMapper,
                                      IAdminAccountEditFormStore adminAccountEditFormStore)
        {
            this.adminAccountRepository = adminAccountRepository;
            this.adminAccountListPageViewModelMapper = adminAccountListPageViewModelMapper;
            this.adminAccountEditFormStore = adminAccountEditFormStore;
        }

        public ActionResult Index()
        {
            var adminAccounts = adminAccountRepository.GetAllAdminAccounts();

            var mappedResults = adminAccountListPageViewModelMapper.CreateSet(adminAccounts);

            return View("Index", new ListPageViewModel<AdminAccountListPageViewModel> {Items = mappedResults.AsQueryable()});
        }

        public ActionResult Edit(string id)
        {
            return View("Edit", adminAccountEditFormStore.GetForm(id));
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