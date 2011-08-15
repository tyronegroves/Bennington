namespace Bennington.AdminAccounts.Models
{
    public interface IAdminAccountEditFormStore
    {
        AdminAccountEditForm GetForm(string id);
        AdminAccountSaveResult SaveForm(AdminAccountEditForm adminAccountEditForm);
    }

    public class AdminAccountSaveResult
    {
        public bool WasANewRecord { get; set; }
    }
}