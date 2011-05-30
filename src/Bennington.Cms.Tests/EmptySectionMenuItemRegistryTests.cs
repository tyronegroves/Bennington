using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bennington.Cms.Models;
using Machine.Specifications;

namespace Bennington.Cms.Tests
{
    [Subject(typeof (EmptySectionMenuItemRegistry))]
    public class when_getting_the_section_items : with_automoqer
    {
        private Establish context =
            () =>
                {
                    registry = Create<EmptySectionMenuItemRegistry>();
                };

        private Because of =
            () => results = registry.GetItems();

        private It should_return_an_empty_set_of_items =
            () => results.Count().ShouldEqual(0);

        private static EmptySectionMenuItemRegistry registry;
        private static IEnumerable<SectionMenuItem> results;
    }
}
