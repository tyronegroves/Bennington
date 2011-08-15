using System.Linq;
using System.Web.Mvc;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Models;
using Should;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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

        [When(@"the administrator submits the following Admin Account edit page")]
        public void WhenTheAdministratorSubmitsTheFollowingAdminAccountEditPage(Table table)
        {
            var controller = CreateTheController();
            var result = controller.Edit(table.CreateInstance<AdminAccountEditForm>());

            ScenarioContext.Current.Set(result);
        }

        [When(@"the administrator deletes the admin account '(.*)'")]
        public void x(string adminAccountId)
        {
            var controller = CreateTheController();

            var result = controller.Delete(adminAccountId);

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

        [Then(@"he should be sent to the admin account list page")]
        public void ThenHeShouldBeSentToTheAdminAccountListPage()
        {
            var actionResult = ScenarioContext.Current.Get<ActionResult>();
            actionResult.ShouldBeType(typeof (RedirectToRouteResult));

            var redirectToRouteResult = actionResult as RedirectToRouteResult;

            redirectToRouteResult.RouteValues.Single(x => x.Key == "controller").Value.ShouldEqual("AdminAccount");
            redirectToRouteResult.RouteValues.Single(x => x.Key == "action").Value.ShouldEqual("Index");
        }
    }
}