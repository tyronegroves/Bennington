using PagedList.Mvc;

namespace Bennington.Core.List
{
    public class ListViewModelOptions
    {
        public ListViewModelOptions()
        {
            SearchAction = "Index";
            TitleViewName = "Title";
            ButtonsViewName = "Buttons";
            PagerViewName = "Pager";
            RowsViewName = "Rows";
            SearchFormViewName = "SearchForm";
            DefaultTitleViewName = "Title";
            DefaultButtonsViewName = "Buttons";
            DefaultPagerViewName = "Pager";
            DefaultRowsViewName = "Rows";
            DefaultSearchFormViewName = "SearchForm";
            ListViewModelIncludeProperties = new[] {"SearchBy", "SearchValue", "PageIndex", "PageSize", "SortBy", "SortDirection"};
        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public PagedListRenderOptions RenderOptions { get; set; }
        public string[] ListViewModelIncludeProperties { get; set; }
        public string[] ListViewModelExcludeProperties { get; set; }
        public string SearchAction { get; set; }
        public string TitleViewName { get; set; }
        public string ButtonsViewName { get; set; }
        public string PagerViewName { get; set; }
        public string RowsViewName { get; set; }
        public string SearchFormViewName { get; set; }
        public string DefaultTitleViewName { get; set; }
        public string DefaultButtonsViewName { get; set; }
        public string DefaultPagerViewName { get; set; }
        public string DefaultRowsViewName { get; set; }
        public string DefaultSearchFormViewName { get; set; }
    }
}