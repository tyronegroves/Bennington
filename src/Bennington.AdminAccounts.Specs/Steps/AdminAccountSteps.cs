using System.Collections.Generic;
using System.Linq;
using Bennington.AdminAccounts.Data;
using Bennington.AdminAccounts.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Bennington.AdminAccounts.Specs.Steps
{
    [Binding]
    public class AdminAccountSteps
    {
        [Given(@"the following admin accounts exist in the database")]
        public void ix(Table table)
        {
            var db = GetTheDatabase();

            db.AdminAccounts.DeleteAll();

            var adminAccounts = table.CreateSet<AdminAccount>();
            foreach (var adminAccount in adminAccounts)
                db.AdminAccounts.Insert(adminAccount);
        }


        [Then(@"the following admin accounts should exist in the database")]
        public void ThenTheFollowingAdminAccountsShouldExistInTheDatabase(Table table)
        {
            var db = GetTheDatabase();

            IEnumerable<AdminAccount> adminAccounts = db["AdminAccounts"].All().Cast<AdminAccount>();

            table.CompareToSet(adminAccounts.ToList());
        }

        private static dynamic GetTheDatabase()
        {
            return ServiceLocatorSteps.ServiceLocator.Resolve<IDatabaseRetriever>().GetTheDatabase();
        }

    }
}