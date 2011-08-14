using System;

namespace Bennington.Cms.Sorting
{
    public interface IPaginationStateRetriever
    {
        PaginationState GetTheCurrentPaginationState(Type type);
    }
}