using Bennington.Core.List;

namespace Bennington.Cms.List
{
    public class CmsListViewModelOptions : ListViewModelOptions
    {
        public CmsListViewModelOptions()
        {
            SearchAction = "Index";
            TitleViewName = "Title";
            ButtonsViewName = "Buttons";
            PagerViewName = "Pager";
            RowsViewName = "Rows";
            SearchFormViewName = "SearchForm";
            DefaultTitleViewName = "~/Views/ListDetail/Title.cshtml";
            DefaultButtonsViewName = "~/Views/ListDetail/Buttons.cshtml";
            DefaultPagerViewName = "~/Views/ListDetail/Pager.cshtml";
            DefaultRowsViewName = "~/Views/ListDetail/Rows.cshtml";
            DefaultSearchFormViewName = "~/Views/ListDetail/SearchForm.cshtml";
        }
    }
}