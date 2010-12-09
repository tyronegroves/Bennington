using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.ContentCaching;
using MvcTurbine.ComponentModel;

namespace TestApplication.Registration
{
    public class ContentProviderRegistration : IServiceRegistration
    {
        private const string ConnectionString = @"data source=np:\\degssql01.deg.local\pipe\MSSQL$SQL2005\sql\query;initial catalog=NAITWEB_develop;";

        public void Register(IServiceLocator locator)
        {
            locator.Register<IContentCacheState>(new TestingContentCacheState());
        }

        public class TestingContentCacheState : IContentCacheState
        {
            public string GetCacheId()
            {
                return "testing";
            }
        }
    }
}