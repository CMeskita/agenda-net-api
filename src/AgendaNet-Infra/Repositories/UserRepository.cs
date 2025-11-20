using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using AgendaNet_Infra.Context;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace AgendaNet_Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostgreContext _context;

        public UserRepository(PostgreContext context)
        {
            _context = context;
        }

        public async Task<User> SaveAsync(User data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task UpdateAsync(User data)
        {
            _context.Users.Update(data);
            _context.Entry(data).Property(p => p.IsActive).IsModified = false;
            _context.Entry(data).Property(p => p.PasswordHash).IsModified = false;
            await _context.SaveChangesAsync();
        }
        public async Task<User> UpdateFirstAcessedAsync(User data)
        {
            data.setAcessed(false);

            _context.Users.Update(data);
            
            _context.Entry(data).Property(p => p.IsActive).IsModified = false;
            _context.Entry(data).Property(p => p.Register).IsModified = false;
            _context.Entry(data).Property(p => p.EstablishmentId).IsModified = false;
            await _context.SaveChangesAsync();
            return data;
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<(List<User> items, int totalitems)> GetAllAsync(int pageindex, int pagesize)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ExistUserCount()
        {
            var result = await _context.Users.AnyAsync();
            return result;
        }
        public async Task<bool> EmailUser(string email)
        {
            var result = await _context.Users.AnyAsync(x => x.Email == email);
            return result;
        }
        public async Task<bool> EmailUserIsAcessed(string email,string establismentid)
        {
            var result = await _context.Users.AnyAsync(x => x.IsAcessed == true && x.EstablishmentId== establismentid);
            return result;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            User user=null;
            var countemail = await _context.EstablishmentTenants.Where(x => x.Email == email).CountAsync();
            if (countemail > 1)
            {
                var teant = _context.EstablishmentTenants.FirstOrDefault(x => x.ItemStore.Equals(1));
                 user = await _context.Users.FirstOrDefaultAsync(x => x.EstablishmentId == teant.EstablishmentId);
            }
            else {       
                user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            }

            return user;

        }
        #region Conctat User
        public async Task<Contact> SaveContactAsync(Contact data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return data;
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
