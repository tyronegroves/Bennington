using System;
using Deg.Alt.Mvc.Mappers;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Registration
{
    public class MapperRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register(typeof(IRouteValueDictionaryToPageLocationMapper), typeof(RouteValueDictionaryToPageLocationMapper));
            locator.Register(typeof(IEmailToMailMessageMapper), typeof(EmailToMailMessageMapper));
            
        }
    }
}