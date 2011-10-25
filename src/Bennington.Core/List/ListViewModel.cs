using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Bennington.Core.List
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            PageSize = 25;
            RenderOptions = new PagedListRenderOptions();
            ModelState = new ModelStateDictionary();
        }

        public ModelStateDictionary ModelState { get; set; }
        public IPagedList Items { get; set; }
        public ListColumns Columns { get; set; }
        public string SearchBy { get; set; }
        public object SearchValue { get; set; }
        public string SearchUrl { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public PagedListRenderOptions RenderOptions { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string TitleViewName { get; set; }
        public string ButtonsViewName { get; set; }
        public string PagerViewName { get; set; }
        public string RowsViewName { get; set; }
        public string SearchViewName { get; set; }
    }
}