using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.ContentCaching;
using MvcTurbine;
using MvcTurbine.Blades;

namespace Deg.Alt.Mvc.Blades
{
    public class ContentCachingBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            try
            {
                var contentCacheState = context.ServiceLocator.Resolve<IContentCacheState>();
                var pageRepository = context.ServiceLocator.Resolve<IPageRepository>();
                var sectionRepository = context.ServiceLocator.Resolve<ISectionRepository>();

                context.ServiceLocator.Register<IPageRepository>(new PageRepositoryCache(pageRepository, contentCacheState));
                context.ServiceLocator.Register<ISectionRepository>(new SectionRepositoryCache(sectionRepository, contentCacheState));
            }
            catch
            {
                return;
            }
        }
    }
}