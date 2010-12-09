using System.Web.Mvc;
using Paragon.Pages.Content;

namespace Paragon.Pages.Binders
{
    public class ContentTreeModelBinder : IModelBinder
    {
        private readonly IContentTreeBuilder contentTreeBuilder;

        public ContentTreeModelBinder(IContentTreeBuilder contentTreeBuilder)
        {
            this.contentTreeBuilder = contentTreeBuilder;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return contentTreeBuilder.GetContentTree();
        }
    }
}