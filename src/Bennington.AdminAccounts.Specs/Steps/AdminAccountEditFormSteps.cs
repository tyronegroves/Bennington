using System.Web.Mvc;
using Bennington.AdminAccounts.Models;
using Should;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Bennington.AdminAccounts.Specs.Steps
{
    [Binding]
    public class AdminAccountEditFormSteps
    {
        [Then(@"he should see an admin account edit form with the following values")]
        public void ThenHeShouldSeeAnAdminAccountEditFormWithTheFollowingValues(Table table)
        {
            var actionResult = ScenarioContext.Current.Get<ActionResult>();
            var viewResult = ((ViewResult) actionResult);

            viewResult.Model.ShouldBeType(typeof (AdminAccountEditForm));

            var editForm = (AdminAccountEditForm) viewResult.Model;

            table.CompareToInstance(editForm);
        }
    }
}