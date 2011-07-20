using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IEditPageButtonRegistry
    {
    }

    public interface IEditPageButtonRegistry<T> : IEditPageButtonRegistry
    {
        IEnumerable<Button> GetTheActionButtons(T @object);
    }
}