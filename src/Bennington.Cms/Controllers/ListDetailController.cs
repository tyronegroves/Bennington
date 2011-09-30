using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using Bennington.Cms.Models;
using PagedList;

namespace Bennington.Cms.Controllers
{
    public abstract class ListDetailController<TItem> : Controller
    {
        public virtual ActionResult Index()
        {
            var gridViewModel = new GridViewModel();
            gridViewModel.Title = GetListTitle(GetType().Name);
            gridViewModel.Columns = GetColumns();

            // Not tested
            gridViewModel.Items = new StaticPagedList<object>(GetListItems(), 0, 10, 100);
            gridViewModel.SearchByOptions = new SearchByOptions();
            gridViewModel.SearchByOptions.Options = new Dictionary<string, string>();
            gridViewModel.SearchByOptions.Options.Add("FirstName", "First Name");

            gridViewModel.Subtitle = "GridHeader";
            gridViewModel.TopRightButtons = new List<Button> {new Button {Id = "Create", Text = "Create"}, new Button {Id = "Delete", Text = "Delete"}};

            // Not testedt

            return View("~/Views/ListDetail/Index.cshtml", gridViewModel);
        }

        public virtual string GetListTitle(string typeName)
        {
            var title = typeName.Replace("Controller", "");
            return Regex.Replace(title, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        protected abstract IPagedList<object> GetListItems();

        protected virtual IEnumerable<GridColumn> GetColumns()
        {
            var gridColumns = new List<GridColumn>();

            foreach(PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(TItem)))
            {
                var gridColumn = new GridColumn {Name = property.Name};
                gridColumns.Add(gridColumn);
            }

            return gridColumns;
        }
    }
}