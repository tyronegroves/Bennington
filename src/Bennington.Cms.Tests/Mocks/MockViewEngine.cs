using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockViewEngine : IViewEngine
    {
        private readonly Dictionary<string, IView> views = new Dictionary<string, IView>(StringComparer.InvariantCultureIgnoreCase);

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if(views.ContainsKey(partialViewName))
            {
                return new ViewEngineResult(views[partialViewName], this);
            }

            return new ViewEngineResult(new List<string>());
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if(views.ContainsKey(viewName))
            {
                return new ViewEngineResult(views[viewName], this);
            }


            return new ViewEngineResult(new List<string>());
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            throw new NotImplementedException();
        }

        public void MakeViewExists(string viewName)
        {
            views.Add(viewName, new MockView());
        }

        public void MakeViewExists(string viewName, IView view)
        {
            views.Add(viewName, view);
        }
    }

    public class MockView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}