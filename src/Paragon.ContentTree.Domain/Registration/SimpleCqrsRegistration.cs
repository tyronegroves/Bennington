using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTurbine.ComponentModel;
using NServiceBus;
using SimpleCqrs.Commanding;
using SimpleCqrs.NServiceBus;

namespace Paragon.ContentTree.Domain.Registration
{
	public class SimpleCqrsRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var runtime = new WebRootSimpleCqrsRuntime();

			Configure.WithWeb()
				.DefaultBuilder()
				.BinarySerializer()
				.SimpleCqrs(runtime)
				//	.UseNsbCommandBus()
				//.MsmqTransport()
				//.UnicastBus()
				//    .CreateBus()
				//    .Start()
					;

			var commandBus = runtime.ServiceLocator.Resolve<ICommandBus>();
			locator.Register(commandBus);			
		}
	}
}
