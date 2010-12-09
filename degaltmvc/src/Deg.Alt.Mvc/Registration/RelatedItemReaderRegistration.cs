using Deg.Alt.ContentProvider.RelatedItemReaders;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Registration
{
    public class RelatedItemReaderRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IRelatedItemReader, ContentItemReader>("Deg.Alt.ContentProvider.RelatedItemReaders.ContentItemReader");
            locator.Register<IRelatedItemReader, EmailItemReader>("Deg.Alt.ContentProvider.RelatedItemReaders.EmailItemReader");
            locator.Register<IRelatedItemReader, MetadataItemReader>("Deg.Alt.ContentProvider.RelatedItemReaders.MetadataItemReader");
        }
    }
}