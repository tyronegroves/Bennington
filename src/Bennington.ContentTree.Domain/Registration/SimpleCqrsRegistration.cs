using MvcTurbine.ComponentModel;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.Registration
{
	public class SimpleCqrsRegistration : IServiceRegistration
	{
	    private readonly BenningtonContentTreeSimpleCqrsRuntime benningtonContentTreeSimpleCqrsRuntime;

	    public SimpleCqrsRegistration(BenningtonContentTreeSimpleCqrsRuntime benningtonContentTreeSimpleCqrsRuntime)
	    {
	        this.benningtonContentTreeSimpleCqrsRuntime = benningtonContentTreeSimpleCqrsRuntime;
	    }

	    public void Register(IServiceLocator locator)
		{
			benningtonContentTreeSimpleCqrsRuntime.Start();

            var commandBus = benningtonContentTreeSimpleCqrsRuntime.ServiceLocator.Resolve<ICommandBus>();
			locator.Register(commandBus);

			locator.Register<SimpleCqrs.IServiceLocator>(benningtonContentTreeSimpleCqrsRuntime.ServiceLocator);
		}
	}
}
