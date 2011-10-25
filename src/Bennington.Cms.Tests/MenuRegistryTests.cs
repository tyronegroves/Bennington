using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Bennington.Cms.Models;
using Bennington.Cms.Models.MenuSystem;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof(MenuRegistry))]
    public class When_a_section_menu_is_retrieved : with_automoqer
    {
        private Establish context = () =>
                                        {
                                            registry = new MenuRegistry();
                                            controllerContext = new ControllerContext();

                                            var sectionMenuItem = new TestSectionMenuItem("Newsletter", "http://www.newsletter.com/", true);

                                            registry.Add(sectionMenuItem);
                                        };

        private Because of = () => sectionMenu = registry.GetSectionMenu(controllerContext);

        private It should_have_the_name_set =
            () => sectionMenu.Items.Single().Name.ShouldEqual("Newsletter");

        private It should_have_the_url_set =
            () => sectionMenu.Items.Single().Url.ShouldEqual("http://www.newsletter.com/");

        private static MenuRegistry registry;
        private static ControllerContext controllerContext;
        private static SectionMenuViewModel sectionMenu;
    }

    [Subject(typeof(MenuRegistry))]
    public class When_a_section_menu_is_retrieved_with_multiple_items : with_automoqer
    {
        private Establish context = () =>
                                        {
                                            registry = new MenuRegistry();
                                            controllerContext = new ControllerContext();

                                            var sectionMenuItem1 = new TestSectionMenuItem("Name1", "http://url1", true);
                                            var sectionMenuItem2 = new TestSectionMenuItem("Name2", "http://url2", false);
                                            var sectionMenuItem3 = new TestSectionMenuItem("Name3", "http://url3", true);

                                            registry.Add(sectionMenuItem1);
                                            registry.Add(sectionMenuItem2);
                                            registry.Add(sectionMenuItem3);
                                        };

        private Because of = () => sectionMenu = registry.GetSectionMenu(controllerContext);

        private It should_have_the_first_items_name_set =
            () => sectionMenu.Items.ElementAt(0).Name.ShouldEqual("Name1");

        private It should_have_the_first_items_url_set =
            () => sectionMenu.Items.ElementAt(0).Url.ShouldEqual("http://url1");

        private It should_have_the_first_items_selected_set =
            () => sectionMenu.Items.ElementAt(0).Selected.ShouldBeTrue();

        private It should_have_the_second_items_name_set =
            () => sectionMenu.Items.ElementAt(1).Name.ShouldEqual("Name2");

        private It should_have_the_second_items_url_set =
            () => sectionMenu.Items.ElementAt(1).Url.ShouldEqual("http://url2");

        private It should_have_the_second_items_selected_set =
            () => sectionMenu.Items.ElementAt(1).Selected.ShouldBeFalse();

        private It should_have_the_third_items_name_set =
            () => sectionMenu.Items.ElementAt(2).Name.ShouldEqual("Name3");

        private It should_have_the_third_items_url_set =
            () => sectionMenu.Items.ElementAt(2).Url.ShouldEqual("http://url3");

        private It should_have_the_third_items_selected_set =
            () => sectionMenu.Items.ElementAt(2).Selected.ShouldBeTrue();

        private static MenuRegistry registry;
        private static ControllerContext controllerContext;
        private static SectionMenuViewModel sectionMenu;
    }

    [Subject(typeof(MenuRegistry))]
    public class When_a_sub_menu_is_retrieved : with_automoqer
    {
        private Establish context = () =>
                                        {
                                            registry = new MenuRegistry();
                                            controllerContext = new ControllerContext();
                                            subMenuItem = new TestSubMenuItem("SubMenu1", "MyUrl1", true, true);
                                            registry.Add(subMenuItem);
                                        };

        private Because of = () => subMenu = registry.GetSubMenu(controllerContext);

        private It should_have_a_item_with_the_name_set =
            () => subMenu.Items.Single().Name.ShouldEqual("SubMenu1");

        private It should_have_a_item_with_the_url_set =
            () => subMenu.Items.Single().Url.ShouldEqual("MyUrl1");

        private It should_have_a_item_with_the_is_selected_set_to_true =
            () => subMenu.Items.ElementAt(0).Selected.ShouldBeTrue();

        private static MenuRegistry registry;
        private static ISubMenuItem subMenuItem;
        private static ControllerContext controllerContext;
        private static SubMenuViewModel subMenu;
    }

    [Subject(typeof(MenuRegistry))]
    public class When_a_sub_menu_is_retrieved_with_multiple_items : with_automoqer
    {
        private Establish context = () =>
                                        {
                                            registry = new MenuRegistry();
                                            controllerContext = new ControllerContext();
                                            subMenuItem1 = new TestSubMenuItem("SubMenu1", "MyUrl1", false, true);
                                            subMenuItem2 = new TestSubMenuItem("SubMenu2", "MyUrl2", true, true);
                                            subMenuItem3 = new TestSubMenuItem("SubMenu3", "MyUrl3", true, true);
                                            registry.Add(subMenuItem1);
                                            registry.Add(subMenuItem2);
                                            registry.Add(subMenuItem3);
                                            registry.Add(new TestSubMenuItem("SubMenu4", "MyUrl4", true, false));
                                        };

        private Because of = () => subMenu = registry.GetSubMenu(controllerContext);

        private It should_have_the_first_item_with_the_name_set =
            () => subMenu.Items.ElementAt(0).Name.ShouldEqual("SubMenu1");

        private It should_have_the_first_item_with_the_url_set =
            () => subMenu.Items.ElementAt(0).Url.ShouldEqual("MyUrl1");

        private It should_have_the_first_item_with_the_is_selected_set_to_false =
            () => subMenu.Items.ElementAt(0).Selected.ShouldBeFalse();

        private It should_have_the_second_item_with_the_name_set =
            () => subMenu.Items.ElementAt(1).Name.ShouldEqual("SubMenu2");

        private It should_have_the_second_item_with_the_url_set =
            () => subMenu.Items.ElementAt(1).Url.ShouldEqual("MyUrl2");

        private It should_have_the_second_item_with_the_is_selected_set_to_true =
            () => subMenu.Items.ElementAt(1).Selected.ShouldBeTrue();

        private It should_have_the_second_item_with_the_is_visible_set_to_true =
            () => subMenu.Items.ElementAt(1).Selected.ShouldBeTrue();

        private It should_have_the_third_item_with_the_name_set =
            () => subMenu.Items.ElementAt(2).Name.ShouldEqual("SubMenu3");

        private It should_have_the_third_item_with_the_url_set =
            () => subMenu.Items.ElementAt(2).Url.ShouldEqual("MyUrl3");

        private It should_have_the_third_item_with_the_is_selected_set_to_true =
            () => subMenu.Items.ElementAt(2).Selected.ShouldBeTrue();

        private It should_have_not_have_a_fourth_item =
            () => subMenu.Items.Count().ShouldEqual(3);

        private static MenuRegistry registry;
        private static ISubMenuItem subMenuItem1;
        private static ISubMenuItem subMenuItem2;
        private static ISubMenuItem subMenuItem3;
        private static ControllerContext controllerContext;
        private static SubMenuViewModel subMenu;
    }

    [Subject(typeof(MenuRegistry))]
    public class When_a_icon_menu_is_retrieved : with_automoqer
    {
        private Establish context = () =>
                                        {
                                            registry = new MenuRegistry();
                                            controllerContext = new ControllerContext();

                                            registry.Add(new TestIconMenuItem("Name1", "Url1", "IconUrl1"));
                                        };

        private Because of = () => iconMenu = registry.GetIconMenu(controllerContext);

        private It should_have_an_item_with_the_name_set =
            () => iconMenu.Items.Single().Name.ShouldEqual("Name1");

        private It should_have_an_item_with_the_url_set =
            () => iconMenu.Items.Single().Url.ShouldEqual("Url1");

        private It should_have_an_item_with_the_icon_url_set =
            () => iconMenu.Items.Single().IconUrl.ShouldEqual("IconUrl1");

        private static MenuRegistry registry;
        private static ControllerContext controllerContext;
        private static IconMenuViewModel iconMenu;
    }

    internal class TestIconMenuItem : IIconMenuItem
    {
        private readonly string name;
        private readonly string url;
        private readonly string iconUrl;

        public TestIconMenuItem(string name, string url, string iconUrl)
        {
            this.name = name;
            this.url = url;
            this.iconUrl = iconUrl;
        }

        public IconMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new IconMenuItemViewModel {Name = name, IconUrl = iconUrl, Url = url};
        }
    }

    internal class TestSectionMenuItem : ISectionMenuItem
    {
        private readonly string name;
        private readonly string url;
        private readonly bool isSelected;

        public TestSectionMenuItem(string name, string url, bool isSelected)
        {
            this.name = name;
            this.url = url;
            this.isSelected = isSelected;
        }

        public string GetName(ControllerContext controllerContext)
        {
            return name;
        }

        public string GetUrl(ControllerContext controllerContext)
        {
            return url;
        }

        public bool IsSelected(ControllerContext controllerContext)
        {
            return isSelected;
        }

        public SectionMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new SectionMenuItemViewModel {Name = name, Selected = isSelected, Url = url};
        }
    }

    internal class TestSubMenuItem : ISubMenuItem
    {
        private readonly string name;
        private readonly string url;
        private readonly bool selected;
        private readonly bool visible;

        public TestSubMenuItem(string name, string url, bool selected, bool visible)
        {
            this.name = name;
            this.url = url;
            this.selected = selected;
            this.visible = visible;
        }

        public SubMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new SubMenuItemViewModel {Name = name, Selected = selected, Url = url, Visible = visible};
        }
    }
}