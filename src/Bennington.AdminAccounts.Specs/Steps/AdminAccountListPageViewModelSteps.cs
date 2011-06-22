using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bennington.AdminAccounts.Models;
using Bennington.Cms.Models;
using Should;
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
            var viewResult = (ViewResult)ScenarioContext.Current.Get<ActionResult>();

            viewResult.Model.ShouldBeType(typeof (ListPageViewModel<AdminAccountListPageViewModel>));

            var listPageViewModel = ((ListPageViewModel<AdminAccountListPageViewModel>) viewResult.Model);

            table.CompareToSet(listPageViewModel.Items);
        }
    }
}
