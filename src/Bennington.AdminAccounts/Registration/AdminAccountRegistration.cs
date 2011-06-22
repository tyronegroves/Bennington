using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Models;
using Bennington.Core.Registration;
using MvcTurbine.ComponentModel;

namespace Bennington.AdminAccounts.Registration
{
    public class AdminAccountRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IAdminAccountRepository, AdminAccountRepository>();
            locator.Register<IAdminAccountListPageViewModelMapper, AdminAccountListPageViewModelMapper>();
        }
    }
}