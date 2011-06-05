using System.Linq;
using Bennington.Cms.Models;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bennington.Cms.Tests
{
    [Subject(typeof (SubMenuRetriever))]
    public class when_no_sub_menu_item_registries_exist : submenuretriever_tests
    {
        private Establish context =
            () =>
                {
                    TheseAreTheRegistries(new ISubMenuItemRegistry[] {});

                    retriever = Create<SubMenuRetriever>();
                };

        private Because of =
            () => result = retriever.GetTheSubMenu();

        private It should_return_a_sub_menu =
            () => result.ShouldNotBeNull();

        private It should_return_no_items =
            () => result.Items.Count().ShouldEqual(0);
    }

    [Subject(typeof (SubMenuRetriever))]
    public class when_one_sub_menu_item_registry_exists_with_two_sub_menu_items : submenuretriever_tests
    {
        private Establish context =
            () =>
                {
                    var registry = new Mock<ISubMenuItemRegistry>();
                    expectedItems = new[] {new SubMenuItem(), new SubMenuItem()};
                    registry.Setup(x => x.GetTheSubMenuItems())
                        .Returns(expectedItems);

                    var subMenuItemRegistries = new[] {registry.Object};
                    TheseAreTheRegistries(subMenuItemRegistries);

                    retriever = Create<SubMenuRetriever>();
                };

        private Because of =
            () => result = retriever.GetTheSubMenu();

        private It should_return_a_sub_menu =
            () => result.ShouldNotBeNull();

        private It should_return_two_items =
            () => result.Items.Count().ShouldEqual(expectedItems.Count());

        private It should_return_the_two_expected_items =
            () =>
                {
                    result.Items.ShouldContain(expectedItems.First());
                    result.Items.ShouldContain(expectedItems.Last());
                };

        private static SubMenuItem[] expectedItems;
    }

    [Subject(typeof(SubMenuRetriever))]
    public class when_two_sub_menu_item_registry_exists_with_one_sub_menu_item_each : submenuretriever_tests
    {
        private Establish context =
            () =>
            {
                expectedItems = new[] { new SubMenuItem(), new SubMenuItem() };

                var firstRegistry = new Mock<ISubMenuItemRegistry>();
                firstRegistry.Setup(x => x.GetTheSubMenuItems())
                    .Returns(new []{expectedItems[0]});

                var secondRegistry = new Mock<ISubMenuItemRegistry>();
                secondRegistry.Setup(x => x.GetTheSubMenuItems())
                    .Returns(new[] {expectedItems[1]});

                var subMenuItemRegistries = new[] { firstRegistry.Object, secondRegistry.Object };
                TheseAreTheRegistries(subMenuItemRegistries);

                retriever = Create<SubMenuRetriever>();
            };

        private Because of =
            () => result = retriever.GetTheSubMenu();

        private It should_return_a_sub_menu =
            () => result.ShouldNotBeNull();

        private It should_return_two_items =
            () => result.Items.Count().ShouldEqual(expectedItems.Count());

        private It should_return_the_two_expected_items =
            () =>
            {
                result.Items.ShouldContain(expectedItems.First());
                result.Items.ShouldContain(expectedItems.Last());
            };

        private static SubMenuItem[] expectedItems;
    }

    public class submenuretriever_tests : with_automoqer
    {
        public static void TheseAreTheRegistries(ISubMenuItemRegistry[] registries)
        {
            GetMock<ISubMenuItemRegistryRetriever>()
                .Setup(x => x.GetTheRegistries())
                .Returns(registries);
        }

        public static SubMenuRetriever retriever;
        public static SubMenu result;
    }
}