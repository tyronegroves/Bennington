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
            var db = ServiceLocatorSteps.ServiceLocator.Resolve<IDatabaseRetriever>().GetTheDatabase();

            db.AdminAccounts.DeleteAll();

            var adminAccounts = table.CreateSet<AdminAccount>();
            foreach (var adminAccount in adminAccounts)
                db.AdminAccounts.Insert(adminAccount);
        }
    }
}