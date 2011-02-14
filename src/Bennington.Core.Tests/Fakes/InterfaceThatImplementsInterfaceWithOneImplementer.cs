namespace Bennington.Core.Tests.Fakes
{
    public interface InterfaceWithOneInterfaceImplementer
    {    
    }

    public interface InterfaceThatImplementsInterfaceWithOneInterfaceImplementer : InterfaceWithOneInterfaceImplementer
    {
    }
}