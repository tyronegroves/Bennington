using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Models;
using Bennington.Cms.Models;
using Machine.Specifications;
using NUnit.Framework;
using Should;

namespace Bennington.AdminAccounts.Specs.Tests
{
    [Subject(typeof (AdminAccountController))]
    public class when_viewing_the_edit_page_of_an_account : with_automoqer
    {
        private Establish context =
            () =>
                {
                    id = "{B9801DE2-3A22-423C-9474-8F70E50911AB}";

                    expectedModel = new AdminAccountEditForm();
                    GetMock<IAdminAccountEditFormStore>()
                        .Setup(x => x.GetForm(id))
                        .Returns(expectedModel);

                    controller = Create<AdminAccountController>();
                };

        private Because of =
            () => result = controller.Edit(id);

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof (ViewResult));

        private It should_return_an_edit_view =
            () => ShouldExtensionMethods.ShouldEqual(((ViewResult) result).ViewName, "Edit");

        private It should_return_the_edit_form_from_the_retriever =
            () => ((ViewResult) result).Model.ShouldBeTheSameAs(expectedModel);

        private static AdminAccountController controller;
        private static string id;
        private static ActionResult result;
        private static AdminAccountEditForm expectedModel;
    }

    [TestFixture]
    public class when_visiting_the_list_page_of_an_account
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

            result.ShouldBeOfType(typeof (ViewResult));
            ObjectAssertExtensions.ShouldEqual(((ViewResult) result).ViewName, "Index");
        }

        [Test]
        public void Index_returns_an_listpageviewmodel_for_accounts()
        {
            var controller = mocker.Create<AdminAccountController>();

            var result = (ViewResult) controller.Index();

            result.Model.ShouldBeType(typeof (ListPageViewModel<AdminAccountListPageViewModel>));
        }

        [Test]
        public void Index_sets_the_items_to_the_result_from_the_view_model_builder()
        {
            var expected = new[] {new AdminAccountListPageViewModel()};

            var adminAccounts = new AdminAccount[] {};

            mocker.GetMock<IAdminAccountRepository>()
                .Setup(x => x.GetAllAdminAccounts())
                .Returns(adminAccounts);

            mocker.GetMock<IAdminAccountListPageViewModelMapper>()
                .Setup(x => x.CreateSet(adminAccounts))
                .Returns(expected);

            var controller = mocker.Create<AdminAccountController>();

            var listPageViewModel = ((ListPageViewModel<AdminAccountListPageViewModel>) ((ViewResult) controller.Index()).Model);

            ObjectAssertExtensions.ShouldEqual(listPageViewModel.Items.Count(), 1);
            listPageViewModel.Items.Contains(expected.Single());
        }
    }
}