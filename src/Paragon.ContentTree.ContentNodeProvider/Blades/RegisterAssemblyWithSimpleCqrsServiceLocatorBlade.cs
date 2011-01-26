using MvcTurbine;
using MvcTurbine.Blades;
using Paragon.ContentTree.ContentNodeProvider.Denormalizers;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;
using SimpleCqrs;

namespace Paragon.ContentTree.ContentNodeProvider.Blades
{
	public class RegisterAssemblyWithSimpleCqrsServiceLocatorBlade : IBlade
	{
		private readonly IServiceLocator simpleCqrsServiceLocator;

		public RegisterAssemblyWithSimpleCqrsServiceLocatorBlade(SimpleCqrs.IServiceLocator simpleCqrsServiceLocator)
		{
			this.simpleCqrsServiceLocator = simpleCqrsServiceLocator;
		}

		public void Dispose()
		{
		}

		public void Initialize(IRotorContext context)
		{
		}

		public void Spin(IRotorContext context)
		{
			SimpleCqrs.ServiceLocator.Current.Register<IContentNodeProviderDraftRepository, ContentNodeProviderDraftRepository>();
			SimpleCqrs.ServiceLocator.Current.Register<IContentNodeProviderPublishedVersionRepository, ContentNodeProviderPublishedVersionRepository>();
			SimpleCqrs.ServiceLocator.Current.Register<Paragon.ContentTree.ContentNodeProvider.Data.IDataModelDataContext, Data.ContentTreeNodeProviderDataModelDataContext>();
			SimpleCqrs.ServiceLocator.Current.Register<IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper, ContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>();
			
			//simpleCqrsServiceLocator.Register(context.ServiceLocator.Resolve<IContentNodeProviderDraftRepository>());
			//simpleCqrsServiceLocator.Register(context.ServiceLocator.Resolve<ContentNodeProviderPublishDenormalizer>());
		}
	}
}