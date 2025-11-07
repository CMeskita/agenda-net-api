using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using AgendaNet_Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace AgendaNet_Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostgreContext _context;

        public UserRepository(PostgreContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(User data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User data)
        {
            _context.Users.Update(data);
            _context.Entry(data).Property(p => p.IsActive).IsModified = false;
            _context.Entry(data).Property(p => p.PasswordHash).IsModified = false;
            await _context.SaveChangesAsync();
        }
        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<(List<User> items, int totalitems)> GetAllAsync(int pageindex, int pagesize)
        {
            throw new NotImplementedException();
        }
        #region Conctatt User
        public async Task SaveContactAsync(Contact data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public Task UpdateContactAsync(Contact data)
        {
            throw new NotImplementedException();
        }
        public Task<(List<Contact> items, int totalitems)> GetAllContactAsync(int pageindex, int pagesize)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteConctactAsync(string id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
