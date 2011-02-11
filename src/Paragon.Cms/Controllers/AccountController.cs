using System;
using System.Web.Mvc;
using MvcTurbine.MembershipProvider;
using MvcTurbine.MembershipProvider.Contexts;
using MvcTurbine.MembershipProvider.PrincipalHelpers;
using SampleApplication.Models;

namespace Paragon.Cms.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
    	private IPrincipalCreator principalCreator;
    	private IPrincipalContext principalContext;
    	public IMembershipService MembershipService { get; set; }
        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public AccountController(IMembershipService membershipService, IPrincipalCreator principalCreator, IPrincipalContext principalContext)
        {
        	this.principalContext = principalContext;
        	this.principalCreator = principalCreator;
        	MembershipService = membershipService;
        }

    	public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    MembershipService.LogInAsUser(model.UserName, model.Password);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
						return new RedirectResult("/Manage");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {

			principalContext.SetPricipal(principalCreator.CreateUnauthenticatedPrincipal());
			return new RedirectResult("/Manage");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //// Attempt to register the user
                //MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                //if (createStatus == MembershipCreateStatus.Success)
                //{
                //    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                //}
            }

            // If we got this far, something failed, redisplay form
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                //if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                //{
                //    return RedirectToAction("ChangePasswordSuccess");
                //}
                //else
                //{
                //    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                //}
            }

            // If we got this far, something failed, redisplay form
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
