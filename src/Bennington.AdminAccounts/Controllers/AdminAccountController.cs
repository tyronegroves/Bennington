using System;
using System.Linq;
using System.Web.Mvc;
using Bennington.AdminAccounts.Helpers;
using Bennington.AdminAccounts.Mappers;
using Bennington.AdminAccounts.Models;

namespace Bennington.AdminAccounts.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly IAdminAccountRepository adminAccountRepository;
        private readonly IAdminAccountListPageViewModelMapper adminAccountListPageViewModelMapper;
        private readonly IAdminAccountEditFormStore adminAccountEditFormStore;
        private readonly IAdminAccountIdGenerator adminAccountIdGenerator;

        public AdminAccountController(IAdminAccountRepository adminAccountRepository,
                                      IAdminAccountListPageViewModelMapper adminAccountListPageViewModelMapper,
                                      IAdminAccountEditFormStore adminAccountEditFormStore,
                                      IAdminAccountIdGenerator adminAccountIdGenerator)
        {
            this.adminAccountRepository = adminAccountRepository;
            this.adminAccountListPageViewModelMapper = adminAccountListPageViewModelMapper;
            this.adminAccountEditFormStore = adminAccountEditFormStore;
            this.adminAccountIdGenerator = adminAccountIdGenerator;
        }

        public ActionResult Index()
        {
            var adminAccounts = adminAccountRepository.GetAllAdminAccounts();

            var mappedResults = adminAccountListPageViewModelMapper.CreateSet(adminAccounts);

            return View("Index", new AdminAccountListPageViewModel {Items = mappedResults.AsQueryable()});
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("Edit", new AdminAccountEditForm
                                        {
                                            Id = adminAccountIdGenerator.GenerateId().ToString()
                                        });
            return View("Edit", adminAccountEditFormStore.GetForm(id));
        }

        [HttpPost]
        public ActionResult Edit(AdminAccountEditForm adminAccountEditForm, bool saveAndExit = false)
        {
            if (ModelState.IsValid == false)
                return View("Edit", adminAccountEditForm);

            adminAccountEditFormStore.SaveForm(adminAccountEditForm);

            if (saveAndExit)
                return RedirectToAction("Index", "AdminAccount");
            return View("Edit", adminAccountEditForm);
        }

        public ActionResult Delete(string id)
        {
            adminAccountEditFormStore.DeleteForm(id);
            return RedirectToAction("Index", "AdminAccount");
        }
    }
}