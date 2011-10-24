using SisoDb;
using SisoDb.Sql2008;

namespace Bennington.Core.SisoDb
{
    public abstract class DatabaseFactory
    {
        protected ISisoDatabase database;

        public DatabaseFactory()
        {
            database = new Sql2008DbFactory().CreateDatabase(new SisoConnectionInfo("Samples"));
        }
    }
}