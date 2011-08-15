using System;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Helpers;
using Bennington.AdminAccounts.Mappers;
using Bennington.AdminAccounts.Models;
using Bennington.Cms.Models;
using Machine.Specifications;
using NUnit.Framework;

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

    [Subject(typeof (AdminAccountController))]
    public class when_viewing_the_create_page_of_an_account : with_automoqer
    {
        private Establish context =
            () =>
                {
                    id = "";

                    GetMock<IAdminAccountIdGenerator>()
                        .Setup(x => x.GenerateId())
                        .Returns(new Guid("A7C9EE09-4968-46E1-B25E-0301953A56E1"));

                    controller = Create<AdminAccountController>();
                };

        private Because of =
            () => result = controller.Edit(id);

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof (ViewResult));

        private It should_return_an_edit_view =
            () => ShouldExtensionMethods.ShouldEqual(((ViewResult) result).ViewName, "Edit");

        private It should_return_an_edit_form =
            () => ((ViewResult) result).Model.ShouldBeOfType(typeof (AdminAccountEditForm));

        private It should_set_the_id =
            () => ((AdminAccountEditForm) ((ViewResult) result).Model).Id.ShouldEqual("a7c9ee09-4968-46e1-b25e-0301953a56e1");

        private static AdminAccountController controller;
        private static string id;
        private static ActionResult result;
        private static AdminAccountEditForm expectedModel;
    }

    [Subject(typeof (AdminAccountController))]
    public class when_saving_an_existing_form : with_automoqer
    {
        private Establish context =
            () =>
                {
                    form = new AdminAccountEditForm();
                    controller = Create<AdminAccountController>();
                };

        private Because of =
            () => result = controller.Edit(form);

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof (ViewResult));

        private It should_return_an_edit_view =
            () => ShouldExtensionMethods.ShouldEqual(((ViewResult) result).ViewName, "Edit");

        private It should_return_the_edit_form_from_the_retriever =
            () => ((ViewResult) result).Model.ShouldBeTheSameAs(form);

        private It should_save_the_form =
            () => GetMock<IAdminAccountEditFormStore>().Verify(x => x.SaveForm(form), Moq.Times.Once());

        private static AdminAccountController controller;
        private static string id;
        private static ActionResult result;
        private static AdminAccountEditForm expectedModel;
        private static AdminAccountEditForm form;
    }

    [Subject(typeof(AdminAccountController))]
    public class when_saving_an_existing_form_and_exiting : with_automoqer
    {
        private Establish context =
            () =>
            {
                form = new AdminAccountEditForm();
                controller = Create<AdminAccountController>();
            };

        private Because of =
            () => result = controller.Edit(form, true);

        private It should_return_a_redirect_to_route_result =
            () => result.ShouldBeOfType(typeof(RedirectToRouteResult));

        private It should_save_the_form =
            () => GetMock<IAdminAccountEditFormStore>().Verify(x => x.SaveForm(form), Moq.Times.Once());

        private static AdminAccountController controller;
        private static string id;
        private static ActionResult result;
        private static AdminAccountEditForm expectedModel;
        private static AdminAccountEditForm form;
    }

    [Subject(typeof(AdminAccountController))]
    public class when_deleting_an_account : with_automoqer
    {
        private Establish context =
            () =>
                {
                    id = "{3FF6E712-14F8-4D42-A892-A13FC609942A}";

                    controller = Create<AdminAccountController>();
                };

        private Because of =
            () => result = controller.Delete(id);

        private It should_return_a_redirect_result =
            () => result.ShouldBeOfType(typeof (RedirectToRouteResult));

        private It should_delete_the_id_from_the_store =
            () => GetMock<IAdminAccountEditFormStore>().Verify(x => x.DeleteForm(id), Moq.Times.Once());

        private static string id;
        private static AdminAccountController controller;
        private static ActionResult result;
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
            ((ViewResult) result).ViewName.ShouldEqual( "Index");
        }

        [Test]
        public void Index_returns_an_listpageviewmodel_for_accounts()
        {
            var controller = mocker.Create<AdminAccountController>();

            var result = (ViewResult) controller.Index();

            result.Model.ShouldBeOfType(typeof (AdminAccountListPageViewModel));
        }

        [Test]
        public void Index_sets_the_items_to_the_result_from_the_view_model_builder()
        {
            var expected = new[] {new AdminAccountListPageItem()};

            var adminAccounts = new AdminAccount[] {};

            mocker.GetMock<IAdminAccountRepository>()
                .Setup(x => x.GetAllAdminAccounts())
                .Returns(adminAccounts);

            mocker.GetMock<IAdminAccountListPageViewModelMapper>()
                .Setup(x => x.CreateSet(adminAccounts))
                .Returns(expected);

            var controller = mocker.Create<AdminAccountController>();

            var listPageViewModel = ((ListPageViewModel<AdminAccountListPageItem>) ((ViewResult) controller.Index()).Model);

            listPageViewModel.Items.Count().ShouldEqual(1);
            listPageViewModel.Items.Contains(expected.Single());
        }
    }
}