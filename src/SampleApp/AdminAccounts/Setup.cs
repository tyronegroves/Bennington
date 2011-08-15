using System;
using Bennington.AdminAccounts;
using MvcTurbine.ComponentModel;

namespace SampleApp.AdminAccounts
{
    public class Setup : IServiceRegistration, IAdminAccountSettings
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IAdminAccountSettings>(this);
        }

        public string ConnectionString
        {
            get { return @"Data Source=.\SQLEXPRESS;Initial Catalog=test;Trusted_Connection=True;"; }
        }

        public string PasswordHash
        {
            get { return @"4uGq/30m8LU3QWt10lTSZQ=="; }
        }
    }
}