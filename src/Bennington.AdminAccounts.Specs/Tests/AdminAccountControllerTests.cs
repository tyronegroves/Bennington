using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Models;
using Bennington.AdminAccounts.Specs.Steps;
using Bennington.Cms.Models;
using NUnit.Framework;
using Should;

namespace Bennington.AdminAccounts.Specs.Tests
{
    [TestFixture]
    public class AdminAccountControllerTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Index_returns_an_index_view_result()
        {
            var controller = mocker.Create<AdminAccountController>();
            var result = controller.Index();

            result.ShouldBeType(typeof (ViewResult));
            ((ViewResult) result).ViewName.ShouldEqual("Index");
        }

        [Test]
        public void Index_returns_an_listpageviewmodel_for_accounts()
        {
            var controller = mocker.Create<AdminAccountController>();

            var result = (ViewResult)controller.Index();

            result.Model.ShouldBeType(typeof (ListPageViewModel<AdminAccountListPageViewModel>));
        }

        [Test]
        public void Index_sets_the_items_to_the_result_from_the_view_model_builder()
        {
            var expected = new[] { new AdminAccountListPageViewModel()};

            var adminAccounts = new AdminAccount[] {};

            mocker.GetMock<IAdminAccountRepository>()
                .Setup(x => x.GetAllAdminAccounts())
                .Returns(adminAccounts);

            mocker.GetMock<IAdminAccountListPageViewModelMapper>()
                .Setup(x => x.CreateSet(adminAccounts))
                .Returns(expected);

            var controller = mocker.Create<AdminAccountController>();

            var listPageViewModel = ((ListPageViewModel<AdminAccountListPageViewModel>) ((ViewResult) controller.Index()).Model);

            listPageViewModel.Items.Count().ShouldEqual(1);
            listPageViewModel.Items.Contains(expected.Single());

        }
    }
}