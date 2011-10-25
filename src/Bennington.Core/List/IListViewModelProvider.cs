using System;
using System.Web.Mvc;

namespace Bennington.Core.List
{
    public interface IListViewModelProvider
    {
        ListViewModel GetListViewModelForType(Type type, ControllerContext controllerContext, ListViewModelOptions options);
    }
}