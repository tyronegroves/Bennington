using System;

namespace Bennington.Cms.Sorting
{
    public interface ISearchStateRetriever
    {
        SearchState GetTheCurrnetSearchState(Type type);
    }
}