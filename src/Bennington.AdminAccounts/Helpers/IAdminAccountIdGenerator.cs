using System;

namespace Bennington.AdminAccounts.Helpers
{
    public interface IAdminAccountIdGenerator
    {
        Guid GenerateId();
    }

    public class AdminAccountIdGenerator : IAdminAccountIdGenerator
    {
        public Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}