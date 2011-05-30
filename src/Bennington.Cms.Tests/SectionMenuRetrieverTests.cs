using System.Collections.Generic;
using Bennington.Cms.Models;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof (SectionMenuRetriever))]
    public class when_retrieving_the_section_menu : with_automoqer
    {
        private Establish context =
            () =>
                {
                    expectedItems = new SectionMenuItem[] {};
                    GetMock<ISectionMenuItemRegistry>()
                        .Setup(x => x.GetItems())
                        .Returns(expectedItems);

                    retriever = Create<SectionMenuRetriever>();
                };

        private Because of =
            () => result = retriever.GetTheSectionMenu();

        private It should_return_a_section_menu =
            () => result.ShouldNotBeNull();

        private It should_return_the_options_from_the_section_menu_registry =
            () => result.Items.ShouldBeTheSameAs(expectedItems);

        private static SectionMenuRetriever retriever;
        private static SectionMenu result;
        private static IEnumerable<SectionMenuItem> expectedItems;
    }
}