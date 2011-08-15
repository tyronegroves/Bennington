using Bennington.AdminAccounts.Registration;
using MvcTurbine.Unity;
using TechTalk.SpecFlow;

namespace Bennington.AdminAccounts.Specs.Steps
{
    [Binding]
    public class ServiceLocatorSteps
    {
        public static MvcTurbine.ComponentModel.IServiceLocator ServiceLocator;

        [BeforeTestRun]
        public static void Setup()
        {
            ServiceLocator = new UnityServiceLocator();

            var registration = new AdminAccountRegistration();
            registration.Register(ServiceLocator);

            ServiceLocator.Register<IAdminAccountSettings>(new TestAdminAccountSettings());
        }
    }

    public class TestAdminAccountSettings : IAdminAccountSettings
    {
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