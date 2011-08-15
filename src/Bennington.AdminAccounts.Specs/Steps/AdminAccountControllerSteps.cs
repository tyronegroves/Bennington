using System.Web.Mvc;
using Bennington.AdminAccounts.Controllers;
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
            var controller = CreateTheController();
            var result = controller.Index();

            ScenarioContext.Current.Set(result);
        }

        [When(@"the administrator visits the Admin Account edit page for '(.*)'")]
        public void WhenTheAdministratorVisitsTheAdminAccountEditPage(string adminAccountId)
        {
            var controller = CreateTheController();
            var result = controller.Edit(adminAccountId);

            ScenarioContext.Current.Set(result);
        }

        private AdminAccountController CreateTheController()
        {
            return ServiceLocatorSteps.ServiceLocator.Resolve<AdminAccountController>();
        }

        [Then(@"he should see the Admin Account list page")]
        public void ThenHeShouldSeeTheAdminAccountListPage()
        {
            var actionResult = ScenarioContext.Current.Get<ActionResult>();
            actionResult.ShouldBeType(typeof (ViewResult));

            ((ViewResult) actionResult).ViewName.ShouldEqual("Index");
        }

        [Then(@"he should see the Admin Account edit page")]
        public void ThenHeShouldSeeTheAdminAccountEditPage()
        {
            var actionResult = ScenarioContext.Current.Get<ActionResult>();
            actionResult.ShouldBeType(typeof (ViewResult));

            ((ViewResult) actionResult).ViewName.ShouldEqual("Edit");
        }
    }
}