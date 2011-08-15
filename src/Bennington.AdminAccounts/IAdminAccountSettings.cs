namespace Bennington.AdminAccounts
{
    public interface IAdminAccountSettings
    {
        string ConnectionString { get; }
        string PasswordHash { get;  }
    }
}