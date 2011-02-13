using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTurbine.ComponentModel;

namespace Paragon.Core
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
