using MvcTurbine.ComponentModel;

namespace Bennington.Core
{
	public interface IServiceLocatorWrapper
	{
		System.Collections.Generic.IList<T> ResolveServices<T>() where T : class;
	}

	public class ServiceLocatorWrapper : IServiceLocatorWrapper
	{
		private readonly IServiceLocator serviceLocator;

		public ServiceLocatorWrapper(IServiceLocator serviceLocator)
		{
			this.serviceLocator = serviceLocator;
		}

		public System.Collections.Generic.IList<T> ResolveServices<T>() where T : class
		{
			return serviceLocator.ResolveServices<T>();
		}
	}
}
