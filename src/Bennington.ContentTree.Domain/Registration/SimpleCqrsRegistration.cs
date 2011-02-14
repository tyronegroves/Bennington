using MvcTurbine.ComponentModel;
using NServiceBus;
using SimpleCqrs.Commanding;
using SimpleCqrs.NServiceBus;

namespace Bennington.ContentTree.Domain.Registration
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

			locator.Register<SimpleCqrs.IServiceLocator>(runtime.ServiceLocator);
		}
	}
}
