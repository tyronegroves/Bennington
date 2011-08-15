using System.Web.Mvc;
using Bennington.AdminAccounts.Models;
using Bennington.Cms.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Bennington.AdminAccounts.Specs.Steps
{
    [Binding]
    public class AdminAccountListPageViewModelSteps
    {
        [Then(@"he should see the following accounts on the list page")]
        public void ThenHeShouldSeeTheFollowingAccountsOnTheListPage(Table table)
        {
            var viewResult = (ViewResult) ScenarioContext.Current.Get<ActionResult>();

            var listPageViewModel = ((ListPageViewModel<AdminAccountListPageItem>) viewResult.Model);

            table.CompareToSet(listPageViewModel.Items);
        }
    }
}