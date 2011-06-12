using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IEditPageButtonRegistry
    {
        IEnumerable<Button> GetTheTopRightButtons();
        IEnumerable<Button> GetTheBottomRightButtons();
    }

    public interface IEditPageButtonRegistry<T> : IEditPageButtonRegistry
    {
    }
}