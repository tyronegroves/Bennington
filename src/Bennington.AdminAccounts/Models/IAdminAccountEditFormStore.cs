namespace Bennington.AdminAccounts.Models
{
    public interface IAdminAccountEditFormStore
    {
        AdminAccountEditForm GetForm(string id);
    }
}