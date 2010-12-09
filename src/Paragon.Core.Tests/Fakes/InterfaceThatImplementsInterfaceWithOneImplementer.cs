namespace Paragon.Core.Tests.Fakes
{
    public interface InterfaceWithOneInterfaceImplementer
    {    
    }

    public interface InterfaceThatImplementsInterfaceWithOneInterfaceImplementer : InterfaceWithOneInterfaceImplementer
    {
    }
}