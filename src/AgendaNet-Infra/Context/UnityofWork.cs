using AgendaNet_Domain.Interfaces;
using AgendaNet_Infra.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
namespace AgendaNet_Infra.Context
{
    public class UnityofWork : IUnitofWork
    {
        private readonly PostgreContext _context;
        private IDbContextTransaction _transaction;
        private IEstablishmentRepository? _establishment;


        public UnityofWork(PostgreContext context)
        {
            _context = context;

        }
        public IEstablishmentRepository EstablishmentRepository => _establishment ??= new EstablishmentRepository(_context);

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
