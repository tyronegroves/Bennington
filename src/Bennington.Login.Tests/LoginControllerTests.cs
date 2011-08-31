using System.Linq;
using System.Web.Mvc;
using Bennington.Login.Controllers;
using Bennington.Login.Models;
using Machine.Specifications;
using Moq;
using MvcTurbine.MembershipProvider;
using It = Machine.Specifications.It;

namespace Bennington.Login.Tests
{
    [Subject(typeof (LoginController))]
    public class when_visiting_the_login_page
    {
        private Establish context =
            () => { controller = new LoginController(null); };

        private Because of =
            () => result = controller.Index();

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof (ViewResult));

        private It should_return_an_index_view =
            () => ((ViewResult) result).ViewName.ShouldEqual("Index");

        private It should_return_a_login_form_as_the_model =
            () => ((ViewResult) result).Model.ShouldBeOfType(typeof (LoginForm));

        private static LoginController controller;
        private static ActionResult result;
    }

    [Subject(typeof (LoginController))]
    public class when_the_credentials_are_valid
    {
        private Establish context =
            () =>
                {
                    membershipService = new Mock<IMembershipService>();
                    membershipService
                        .Setup(x => x.ValidateUser("u", "p"))
                        .Returns(true);

                    loginForm = new LoginForm
                                    {
                                        Username = "u",
                                        Password = "p"
                                    };

                    controller = new LoginController(membershipService.Object);
                };

        private Because of =
            () => result = controller.Index(loginForm);

        private It should_login_the_user =
            () => membershipService
                      .Verify(x => x.LogInAsUser("u", "p"), Moq.Times.Once());

        private It should_return_a_redirect_result =
            () => result.ShouldBeOfType(typeof (RedirectResult));

        private It should_redirect_to_manage =
            () => ((RedirectResult) result).Url.ShouldEqual("/");

        private static LoginForm loginForm;
        private static LoginController controller;
        private static ActionResult result;
        private static Mock<IMembershipService> membershipService;
    }

    [Subject(typeof (LoginController))]
    public class when_the_credentials_are_not_valid
    {
        private Establish context =
            () =>
                {
                    membershipService = new Mock<IMembershipService>();
                    membershipService
                        .Setup(x => x.ValidateUser("the Username", "the Password"))
                        .Returns(false);

                    loginForm = new LoginForm
                                    {
                                        Username = "the Username",
                                        Password = "the Password"
                                    };

                    controller = new LoginController(membershipService.Object);
                };

        private Because of =
            () => result = controller.Index(loginForm);

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof (ViewResult));

        private It should_return_an_index_result =
            () => ((ViewResult) result).ViewName.ShouldEqual("Index");

        private It should_return_the_same_form =
            () => ((ViewResult) result).Model.ShouldBeTheSameAs(loginForm);

        private It should_have_a_model_state_error_for_username =
            () => ((ViewResult) result).ViewData.ModelState["Username"].Errors
                      .Single().ErrorMessage.ShouldEqual("Login Failed. Please try again.");

        private It should_have_a_model_state_error_for_password =
            () => ((ViewResult) result).ViewData.ModelState["Password"].Errors
                      .Single().ErrorMessage.ShouldEqual("Login Failed. Please try again.");

        private It should_not_login =
            () => membershipService.Verify(x => x.LogInAsUser(Moq.It.IsAny<string>(),
                                                              Moq.It.IsAny<string>()), Moq.Times.Never());

        private static LoginForm loginForm;
        private static LoginController controller;
        private static ActionResult result;
        private static Mock<IMembershipService> membershipService;
    }
}