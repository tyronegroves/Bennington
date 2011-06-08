using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IButtonRegistry
    {
        IEnumerable<Button> GetTheButtons();
    }

    public interface IButtonRegistry<T> : IButtonRegistry
    {
    }

    public interface IButtonRegistryForTopRightButtons<T> : IButtonRegistry<T>
    {
    }
}