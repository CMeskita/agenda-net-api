using AgendaNet_Domain.Entities;
namespace AgendaNet_Domain.Interfaces
{
    public interface IUserRepository
    {
        Task SaveAsync(User data);
        Task UpdateAsync(User data);
        Task<bool> DeleteAsync(string id);
        Task<(List<User> items, int totalitems)> GetAllAsync(int pageindex, int pagesize);

        #region Conctatt User
        Task SaveContactAsync(Contact data);
        Task UpdateContactAsync(Contact data);
        Task<bool> DeleteConctactAsync(string id);
        Task<(List<Contact> items, int totalitems)> GetAllContactAsync(int pageindex, int pagesize);
        #endregion

    }
}
