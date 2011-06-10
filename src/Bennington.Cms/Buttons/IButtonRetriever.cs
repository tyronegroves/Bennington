using System;
using System.Collections.Generic;

namespace Bennington.Cms.Buttons
{
    public interface IButtonRetriever
    {
        IEnumerable<Button> GetButtonsForTopRightOfListPage(Type modelType);
        IEnumerable<Button> GetButtonsForBottomLeftOfListPage(Type modelType);
    }
}