using System.Linq;
using Bennington.Cms.Helpers;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagedList;

namespace Bennington.Cms.Tests
{
    [TestClass]
    public class PaginationHelpers
    {
        [TestMethod]
        public void Returns_nothing_when_there_are_no_pages()
        {
            new string[] {}.ToPagedList(0, 10)
                .GetPageRange(0).ShouldBeNull();
        }

        [TestMethod]
        public void Returns_1_to_2_when_there_are_two_items_and_page_size_is_10()
        {
            var paginationRange = new[] {"a", "b"}.ToPagedList(0, 10).GetPageRange(0);
            paginationRange.Begin.ShouldEqual(1);
            paginationRange.End.ShouldEqual(2);
        }

        [TestMethod]
        public void Returns_1_to_10_when_on_first_page_and_there_are_11_items_in_the_list_and_page_size_is_10()
        {
            var paginationRange = Enumerable.Range(0, 11).ToPagedList(0, 10).GetPageRange(0);
            paginationRange.Begin.ShouldEqual(1);
            paginationRange.End.ShouldEqual(10);
        }

        [TestMethod]
        public void Returns_11_to_12_when_on_second_page_and_there_are_12_items_in_the_list_and_page_size_is_10()
        {
            var paginationRange = Enumerable.Range(0, 12).ToPagedList(0, 10).GetPageRange(1);
            paginationRange.Begin.ShouldEqual(11);
            paginationRange.End.ShouldEqual(12);
        }

        [TestMethod]
        public void Returns_11_to_20_when_on_second_page_and_there_are_21_items_in_the_list()
        {
            var paginationRange = Enumerable.Range(0, 21).ToPagedList(0, 10).GetPageRange(1);
            paginationRange.Begin.ShouldEqual(11);
            paginationRange.End.ShouldEqual(20);
        }

        [TestMethod]
        public void Returns_nothing_when_the_index_is_outside_the_range_of_the_items()
        {
            var paginationRange = Enumerable.Range(0, 21).ToPagedList(0, 10).GetPageRange(3);
            paginationRange.ShouldBeNull();
        }

        [TestMethod]
        public void ToString_returns_the_First_dash_last()
        {
            var paginationRange = Enumerable.Range(0, 2).ToPagedList(0, 10).GetPageRange(0).ToString();
            paginationRange.ShouldEqual("1 - 2");
        }

        [TestMethod]
        public void ToSTring_returns_a_single_number_when_both_match()
        {
            var paginationRange = Enumerable.Range(0, 11).ToPagedList(0, 10).GetPageRange(1).ToString();
            paginationRange.ShouldEqual("11");
        }
    }
}