using MvcTurbine;
using MvcTurbine.Blades;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
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
			simpleCqrsServiceLocator.Register(context.ServiceLocator.Resolve<IContentNodeProviderDraftRepository>());
		}
	}
}