using System.Linq;
using Bennington.Cms.Models;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof (SubMenuRetriever))]
    public class when_no_sub_menu_item_registries_exist : submenuretriever_tests
    {
        private Establish context =
            () =>
                {
                    TheseAreTheRegistries(new SubMenuItemRegistry[] {});

                    retriever = Create<SubMenuRetriever>();
                };

        private Because of =
            () => result = retriever.GetTheSubMenu();

        private It should_return_a_sub_menu =
            () => result.ShouldNotBeNull();

        private It should_return_no_items =
            () => result.Items.Count().ShouldEqual(0);
    }

    public class submenuretriever_tests : with_automoqer
    {
        public static void TheseAreTheRegistries(SubMenuItemRegistry[] registries)
        {
            GetMock<ISubMenuItemRegistryRetriever>()
                .Setup(x => x.GetTheRegistries())
                .Returns(registries);
        }

        public static SubMenuRetriever retriever;
        public static SubMenu result;
    }
}