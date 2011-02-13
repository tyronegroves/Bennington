using MvcTurbine;
using MvcTurbine.Blades;
using Paragon.ContentTree.ContentNodeProvider.Denormalizers;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;
using Paragon.Core.Helpers;
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
			SimpleCqrs.ServiceLocator.Current.Register<Data.IDataModelDataContext, Data.DataModelDataContext>();
			SimpleCqrs.ServiceLocator.Current.Register<IXmlFileSerializationHelper, XmlFileSerializationHelper>();
			SimpleCqrs.ServiceLocator.Current.Register<IApplicationSettingsValueGetter, ApplicationSettingsValueGetter>();
			SimpleCqrs.ServiceLocator.Current.Register<IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper, ContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper>();
		}
	}
}