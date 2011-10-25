using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using Bennington.Cms.List;
using Bennington.Core.List;
using PagedList;

namespace Bennington.Cms.Controllers
{
    public abstract class ListDetailController<TListItem, TForm> : Controller
    {
        public virtual ActionResult Index()
        {
            var listViewModel = ListViewModelProviders.Providers.GetListViewModelForType(typeof(TListItem), ControllerContext, new CmsListViewModelOptions());
            OnPreRenderModel(listViewModel);

            return ViewOrDefaultView("List", "~/Views/ListDetail/List.cshtml", listViewModel);
        }

        public ActionResult Create()
        {
            return ViewOrDefaultView("Create", "~/Views/ListDetail/Create.cshtml", CreateForm());
        }

        [HttpPost]
        public ActionResult Create(TForm form)
        {
            if(!ModelState.IsValid)
                return ViewOrDefaultView("Create", "~/Views/ListDetail/Create.cshtml", form);

            InsertForm(form);

            return RedirectToAction("Manage", new {id = GetId(form)});
        }

        public ActionResult Manage()
        {
            var id = ValueProvider.GetValue("id").ConvertTo(GetFormIdType());

            return ViewOrDefaultView("Manage", "~/Views/ListDetail/Manage.cshtml", GetFormById(id));
        }

        [HttpPost]
        public ActionResult Manage(TForm form)
        {
            if(!ModelState.IsValid)
                return ViewOrDefaultView("Manage", "~/Views/ListDetail/Manage.cshtml", form);

            UpdateForm(form);

            return RedirectToAction("Manage", new {id = GetId(form)});
        }

        public ActionResult Delete(object id)
        {
            DeleteItem(id);
            return RedirectToAction("Index");
        }

        public virtual void DeleteItem(object id)
        {
        }

        public virtual void UpdateForm(TForm form)
        {
        }

        public virtual TForm GetFormById(object id)
        {
            return default(TForm);
        }

        public virtual void InsertForm(TForm form)
        {
        }

        public virtual object GetId(object model)
        {
            if(model == null)
                return null;

            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());

            if(modelMetadata.AdditionalValues.ContainsKey("NoIdentifier")) return null;

            if(modelMetadata.AdditionalValues.ContainsKey("Identifier"))
            {
                var propertyName = (string)modelMetadata.AdditionalValues["Identifier"];

                return modelMetadata.Properties.Single(p => p.PropertyName == propertyName).Model;
            }

            return modelMetadata.Properties.Any(p => p.PropertyName == "Id") ? modelMetadata.Properties.Single(p => p.PropertyName == "Id").Model : null;
        }

        public virtual Type GetFormIdType()
        {
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TForm));

            if(modelMetadata.AdditionalValues.ContainsKey("NoIdentifier")) return null;

            if(modelMetadata.AdditionalValues.ContainsKey("Identifier"))
            {
                var propertyName = (string)modelMetadata.AdditionalValues["Identifier"];

                return modelMetadata.Properties.Single(p => p.PropertyName == propertyName).ModelType;
            }

            return modelMetadata.Properties.Any(p => p.PropertyName == "Id") ? modelMetadata.Properties.Single(p => p.PropertyName == "Id").ModelType : null;
        }

        private ActionResult ViewOrDefaultView(string viewName, string defaultViewName, object model)
        {
            var viewEngineResults = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            return viewEngineResults.View != null ? View(viewEngineResults.View, model) : View(defaultViewName, model);
        }

        protected virtual IPagedList<TListItem> GetPagedListItemsCore(ListViewModel listViewModel)
        {
            var listItems = GetListItems(listViewModel);

            if(ModelState.IsValid)
            {
                listItems = ApplyOrdering(listViewModel, listItems);
                listItems = ApplySearchCriteria(listViewModel, listItems);
            }

            return listItems.ToPagedList(listViewModel.PageIndex, listViewModel.PageSize);
        }

        protected virtual IQueryable<TListItem> GetListItems(ListViewModel listViewModel)
        {
            return new List<TListItem>().AsQueryable();
        }

        protected virtual void OnPreRenderModel(ListViewModel listViewModel)
        {
        }

        protected virtual TForm CreateForm()
        {
            return Activator.CreateInstance<TForm>();
        }

        private static IQueryable<TListItem> ApplyOrdering(ListViewModel listViewModel, IQueryable<TListItem> listItems)
        {
            if(listViewModel.Columns.Exists(listViewModel.SortBy))
                listItems = listItems.OrderBy(string.Format("{0} {1}", listViewModel.SortBy, listViewModel.SortDirection));
            return listItems;
        }

        private static IQueryable<TListItem> ApplySearchCriteria(ListViewModel listViewModel, IQueryable<TListItem> listItems)
        {
            if(listViewModel.Columns.Exists(listViewModel.SearchBy) && listViewModel.SearchValue != null)
            {
                var listColumn = listViewModel.Columns.Find(listViewModel.SearchBy);
                string predicate;

                if(listColumn.Type == typeof(string))
                    predicate = string.Format("{0}.StartsWith(@0)", listViewModel.SearchBy);
                else
                    predicate = string.Format("{0} >= @0", listViewModel.SearchBy);

                listItems = listItems.Where(predicate, listViewModel.SearchValue);
            }

            return listItems;
        }
    }
}