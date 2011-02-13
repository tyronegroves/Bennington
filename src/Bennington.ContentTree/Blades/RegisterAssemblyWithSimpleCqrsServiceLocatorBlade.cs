using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTurbine;
using MvcTurbine.Blades;
using Paragon.ContentTree.Registration;
using Paragon.ContentTree.Repositories;
using SimpleCqrs;

namespace Paragon.ContentTree.Blades
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
			simpleCqrsServiceLocator.Register(context.ServiceLocator.Resolve<ITreeNodeRepository>());
		}
	}
}