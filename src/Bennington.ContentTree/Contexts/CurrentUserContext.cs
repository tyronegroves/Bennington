using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bennington.ContentTree.Contexts
{
    public interface ICurrentUserContext
    {
        IPrincipal GetCurrentPrincipal();
    }

    public class CurrentUserContext : ICurrentUserContext
    {
        public IPrincipal GetCurrentPrincipal()
        {
            return HttpContext.Current.User;
        }
    }
}