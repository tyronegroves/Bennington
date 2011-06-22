using System.Web.Mvc;
using Bennington.AdminAccounts.Models;
using Simple.Data;
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
            var db = Database.OpenConnection("Data Source=localhost;Initial Catalog=AGCEVENTREG_TESTING;Trusted_Connection=True;");

            db.AdminAccounts.DeleteAll();

            var adminAccounts = table.CreateSet<AdminAccount>();
            foreach (var adminAccount in adminAccounts)
                db.AdminAccounts.Insert(adminAccount);
        }
    }
}