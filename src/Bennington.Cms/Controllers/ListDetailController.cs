using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Bennington.Cms.Models;
using PagedList;

namespace Bennington.Cms.Controllers
{
    public abstract class ListDetailController<TForm, TViewModel> : Controller where TForm : new()
    {
        public virtual ActionResult Index()
        {
            var gridViewModel = new GridViewModel();
            gridViewModel.Title = GetListTitle(GetType().Name);
            gridViewModel.Columns = GetColumns();
            gridViewModel.Items = GetListItems(1, 1);

            return View("~/Views/ListDetail/Index.cshtml", gridViewModel);
        }

        public virtual ActionResult Create()
        {
            return View("~/Views/ListDetail/Create.cshtml", new TForm());
        }

        [HttpPost]
        public virtual ActionResult Create(TForm form)
        {
            if(!ModelState.IsValid)
                return View("~/Views/ListDetail/Create.cshtml", form);

            CreateItem(form);

            return RedirectToAction("Manage", new {id = GetCurrentItemId()});
        }

        public virtual ActionResult Manage(object id)
        {
            return View("~/Views/ListDetail/Manage.cshtml", GetFormById(id));
        }

        [HttpPost]
        public virtual ActionResult Manage(TForm form)
        {
            if (!ModelState.IsValid)
                return View("~/Views/ListDetail/Manage.cshtml", form);

            SaveItem(form);

            return RedirectToAction("Manage", new {id = GetCurrentItemId()});
        }

        public virtual void CreateItem(TForm form)
        {
        }

        public virtual void SaveItem(TForm form)
        {
        }

        public abstract TForm GetFormById(object id);

        public virtual object GetCurrentItemId()
        {
            return RouteData == null ? null : RouteData.Values["id"];
        }

        public virtual string GetListTitle(string typeName)
        {
            var title = typeName.Replace("Controller", "");
            return Regex.Replace(title, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        public abstract IPagedList<TViewModel> GetListItems(int pageIndex, int pageSize);

        protected virtual IEnumerable<GridColumn> GetColumns()
        {
            var gridColumns = new List<GridColumn>();

            foreach(PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(TViewModel)))
            {
                var gridColumn = new GridColumn {Name = property.Name};
                gridColumns.Add(gridColumn);
            }

            return gridColumns;
        }
    }
}