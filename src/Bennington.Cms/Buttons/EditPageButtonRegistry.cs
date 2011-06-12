using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IEditPageButtonRegistry
    {
        IEnumerable<Button> GetTheActionButtons();
    }

    public interface IEditPageButtonRegistry<T> : IEditPageButtonRegistry
    {
    }
}