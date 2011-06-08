using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IListPageButtonRegistry
    {
        IEnumerable<Button> GetTheTopRightButtons();
    }

    public interface IListPageListPageButtonRegistry<T> : IListPageButtonRegistry
    {
    }
}