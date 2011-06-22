using System.Web.Mvc;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Models;
using Bennington.AdminAccounts.Specs.Tests;
using Should;
using TechTalk.SpecFlow;

namespace Bennington.AdminAccounts.Specs.Steps
{
    [Binding]
    public class AdminAccountControllerSteps
    {
        [When(@"the administrator visits the Admin Account list page")]
        public void WhenTheAdministratorVisitsTheAdminAccountListPage()
        {
            var controller = new AdminAccountController(new AdminAccountRepository(), new AdminAccountListPageViewModelMapper());
            var result = controller.Index();

            ScenarioContext.Current.Set(result);
        }

        [Then(@"he should see the Admin Account list page")]
        public void ThenHeShouldSeeTheAdminAccountListPage()
        {
            var actionResult = ScenarioContext.Current.Get<ActionResult>();
            actionResult.ShouldBeType(typeof (ViewResult));

            ((ViewResult) actionResult).ViewName.ShouldEqual("Index");
        }
    }
}