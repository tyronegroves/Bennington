using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IListPageButtonRegistry
    {
        IEnumerable<Button> GetTheTopRightButtons();
        IEnumerable<Button> GetTheBottomRightButtons();
    }

    public interface IListPageListPageButtonRegistry<T> : IListPageButtonRegistry
    {
    }
}