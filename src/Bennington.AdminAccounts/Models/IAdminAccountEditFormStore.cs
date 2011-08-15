namespace Bennington.AdminAccounts.Models
{
    public interface IAdminAccountEditFormStore
    {
        AdminAccountEditForm GetForm(string id);
        void SaveForm(AdminAccountEditForm adminAccountEditForm);
        void DeleteForm(string id);
    }
}