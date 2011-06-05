using System;
using System.Web.Mvc;
using Bennington.Cms.Controllers;
using Bennington.Cms.Models;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof (MenuSystemController))]
    public class when_viewing_the_section_menu : with_automoqer
    {
        private Establish context =
            () =>
                {
                    sectionMenu = new SectionMenu();
                    GetMock<ISectionMenuRetriever>()
                        .Setup(x => x.GetTheSectionMenu())
                        .Returns(sectionMenu);
                    controller = Create<MenuSystemController>();
                };

        private Because of =
            () => result = controller.SectionMenu();

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof (ViewResult));

        private It should_return_a_SectionMenu_view =
            () => result.CastAs<ViewResult>().ViewName.ShouldEqual("SectionMenu");

        private It should_return_the_section_menu =
            () => result.CastAs<ViewResult>().ViewData.Model.ShouldBeTheSameAs(sectionMenu);

        private static MenuSystemController controller;
        private static ActionResult result;
        private static SectionMenu sectionMenu;
    }

    [Subject(typeof(MenuSystemController))]
    public class when_viewing_the_sub_menu : with_automoqer
    {
        private Establish context =
            () =>
            {
                subMenu = new SubMenu();
                GetMock<ISubMenuRetriever>()
                    .Setup(x => x.GetTheSubMenu())
                    .Returns(subMenu);
                controller = Create<MenuSystemController>();
            };

        private Because of =
            () => result = controller.SubMenu();

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof(ViewResult));

        private It should_return_a_SubMenu_view =
            () => result.CastAs<ViewResult>().ViewName.ShouldEqual("SubMenu");

        private It should_return_the_sub_menu =
            () => result.CastAs<ViewResult>().ViewData.Model.ShouldBeTheSameAs(subMenu);

        private static MenuSystemController controller;
        private static ActionResult result;
        private static SubMenu subMenu;
    }
}