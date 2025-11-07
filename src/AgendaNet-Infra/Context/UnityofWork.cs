using AgendaNet_Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
namespace AgendaNet_Infra.Context
{
    public class UnityofWork : IUnitofWork
    {
        private readonly PostgreContext _context;
        private IDbContextTransaction _transaction;

        public UnityofWork(PostgreContext context, IDbContextTransaction transaction)
        {
            _context = context;

        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
