using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using AgendaNet_Infra.Context;
using Microsoft.EntityFrameworkCore;
namespace AgendaNet_Infra.Repositories
{
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly PostgreContext _context;

        public EstablishmentRepository(PostgreContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Establishment data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Establishment data)
        {
        }
        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<(List<Establishment> items, int totalitems)> GetAllAsync(int pageindex, int pagesize)
        {
            throw new NotImplementedException();
        }

        #region Document
        public async Task SaveDocumentAsync(Document data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public Task UpdateDocumentAsync(Document data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDocumentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<(List<Document> items, int totalitems)> GetAllDocumentAsync(int pageindex, int pagesize)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
