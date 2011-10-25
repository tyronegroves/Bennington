using System;
using System.Web.Mvc;

namespace Bennington.Core.List
{
    public interface IListColumnProvider
    {
        ListColumns GetColumnsForType(Type type, ControllerContext controllerContext);
    }
}