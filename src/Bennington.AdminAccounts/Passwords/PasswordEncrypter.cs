using System;
using System.Security.Cryptography;

namespace Bennington.AdminAccounts.Passwords
{
    public interface IPasswordHasher
    {
        string GetHash(string password);
    }

    public class PasswordHasher : IPasswordHasher
    {
        private readonly IAdminAccountSettings adminAccountSettings;

        public PasswordHasher(IAdminAccountSettings adminAccountSettings)
        {
            this.adminAccountSettings = adminAccountSettings;
        }

        public string GetHash(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            password += adminAccountSettings.PasswordHash;

            var hashAlgorithm = new SHA256CryptoServiceProvider();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = hashAlgorithm.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}