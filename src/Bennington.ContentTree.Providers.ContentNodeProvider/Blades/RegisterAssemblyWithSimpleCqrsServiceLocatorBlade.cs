using System;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using Bennington.Core;
using Bennington.Core.Helpers;
using MvcTurbine;
using MvcTurbine.Blades;
using SimpleCqrs;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Blades
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
			SimpleCqrs.ServiceLocator.Current.Register<ITreeNodeProviderContext, TreeNodeProviderContext>();
			SimpleCqrs.ServiceLocator.Current.Register<IServiceLocatorWrapper, ServiceLocatorWrapper>();
			simpleCqrsServiceLocator.Register(context.ServiceLocator.Resolve<ITreeNodeSummaryContext>());
			simpleCqrsServiceLocator.Register(context.ServiceLocator.Resolve<ITreeNodeProviderContext>());
		}
	}
}