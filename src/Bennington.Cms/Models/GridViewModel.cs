using System.Collections.Generic;
using Bennington.Cms.Buttons;
using PagedList;

namespace Bennington.Cms.Models
{
    public class GridViewModel
    {
        public string Title { get; set; }

        public IEnumerable<GridColumn> Columns { get; set; }


        // Not tested

        public IPagedList<object> Items { get; set; }
        public SearchByOptions SearchByOptions { get; set; }
        public string Subtitle { get; set; } // TODO: Rename subtitle
        public List<Button> TopRightButtons { get; set; }
        public List<Button> BottomLeftButtons { get; set; }
        public bool ViewingAll { get; set; }
    }
}