using Simple.Data;

namespace Bennington.AdminAccounts.Data
{
    public interface IDatabaseRetriever
    {
        dynamic GetTheDatabase();
    }

    public class DatabaseRetriever : IDatabaseRetriever
    {
        private readonly string connectionString;

        public DatabaseRetriever(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public dynamic GetTheDatabase()
        {
            return Database.OpenConnection(connectionString);
        }
    }
}