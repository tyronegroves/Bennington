using System;
using System.Web.Mvc;
using Bennington.Cms.Controllers;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models.MenuSystem;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof(MenuSystemController))]
    public class when_viewing_the_section_menu : with_automoqer
    {
        private Establish context =
            () =>
                {
                    sectionMenu = new SectionMenuViewModel();
                    controllerContext = new ControllerContext();

                    GetMock<IMenuRegistry>()
                        .Setup(x => x.GetSectionMenu(controllerContext))
                        .Returns(sectionMenu);

                    controller = Create<MenuSystemController>();
                    controller.ControllerContext = controllerContext;
                };

        private Because of =
            () => result = controller.SectionMenu() as ViewResult;

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof(ViewResult));

        private It should_return_a_SectionMenu_view =
            () => result.ViewName.ShouldEqual("SectionMenu");

        private It should_return_the_section_menu =
            () => result.ViewData.Model.ShouldBeTheSameAs(sectionMenu);

        private It should_pass_its_controller_context_to_the_get_section_menu_method =
            () => GetMock<IMenuRegistry>().Verify(registry => registry.GetSectionMenu(controllerContext));

        private static MenuSystemController controller;
        private static ViewResult result;
        private static SectionMenuViewModel sectionMenu;
        private static ControllerContext controllerContext;
    }

    [Subject(typeof(MenuSystemController))]
    public class when_viewing_the_sub_menu : with_automoqer
    {
        private Establish context =
            () =>
                {
                    subMenu = new SubMenuViewModel();
                    controllerContext = new ControllerContext();

                    GetMock<IMenuRegistry>()
                        .Setup(x => x.GetSubMenu(controllerContext))
                        .Returns(subMenu);

                    controller = Create<MenuSystemController>();
                    controller.ControllerContext = controllerContext;
                };

        private Because of =
            () => result = controller.SubMenu() as ViewResult;

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof(ViewResult));

        private It should_return_a_SubMenu_view =
            () => result.ViewName.ShouldEqual("SubMenu");

        private It should_return_the_sub_menu =
            () => result.ViewData.Model.ShouldBeTheSameAs(subMenu);

        private It should_pass_its_controller_context_to_the_get_sub_menu_method =
            () => GetMock<IMenuRegistry>().Verify(registry => registry.GetSubMenu(controllerContext));


        private static MenuSystemController controller;
        private static ViewResult result;
        private static SubMenuViewModel subMenu;
        private static ControllerContext controllerContext;
    }

    [Subject(typeof(MenuSystemController))]
    public class when_viewing_the_icon_menu : with_automoqer
    {
        private Establish context =
            () =>
                {
                    iconMenu = new IconMenuViewModel();
                    controllerContext = new ControllerContext();

                    GetMock<IMenuRegistry>()
                        .Setup(x => x.GetIconMenu(controllerContext))
                        .Returns(iconMenu);

                    controller = Create<MenuSystemController>();
                    controller.ControllerContext = controllerContext;
                };

        private Because of =
            () => result = controller.IconMenu() as ViewResult;

        private It should_return_a_view_result =
            () => result.ShouldBeOfType(typeof(ViewResult));

        private It should_return_a_IconMenu_view =
            () => result.ViewName.ShouldEqual("IconMenu");

        private It should_return_the_icon_menu =
            () => result.ViewData.Model.ShouldBeTheSameAs(iconMenu);

        private It should_pass_its_controller_context_to_the_get_icon_menu_method =
            () => GetMock<IMenuRegistry>().Verify(registry => registry.GetIconMenu(controllerContext));

        private static MenuSystemController controller;
        private static ViewResult result;
        private static IconMenuViewModel iconMenu;
        private static ControllerContext controllerContext;
    }
}