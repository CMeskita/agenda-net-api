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

        public async Task<Establishment> SaveAsync(Establishment data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return data;
        }
        public async Task<EstablishmentTenant> TenantSaveAsync(EstablishmentTenant data)
        {
            var existingTenant = await _context.EstablishmentTenants
                .FirstOrDefaultAsync(x => x.Id == data.Id);
            if (existingTenant != null)
            {
                var numberMax = await _context.EstablishmentTenants.Select(x => (int?)x.ItemStore).MaxAsync() ?? 0;
                if (numberMax <= 0)
                {
                    data.SetItemStore(numberMax = 1);
                }
                else
                {
                    data.SetItemStore(numberMax + 1);
                }

            }
            else
            {
                data.SetItemStore(1);
            }

            //var document = JsonSerializer.Serialize(data.AtributosEstablisment);
            data.SetAtributosEstablismentJson(data.Establisment);

            await _context.EstablishmentTenants.AddAsync(data);

            await _context.SaveChangesAsync();

            return data;
        }
        public async Task<EstablishmentTenant> GetEstablishmentTenantMaxAsync(string tenantid)
        {
            // Primeiro: obtém o maior ItemStore ou null se estiver vazio
            var tenantId = await _context.EstablishmentTenants
                 .OrderByDescending(x => x.ItemStore)
                 .Select(x => x.Id)
                 .FirstOrDefaultAsync();

            var maxItemStore = await _context.EstablishmentTenants.MaxAsync(x => x.ItemStore);

            return await _context.EstablishmentTenants.FirstOrDefaultAsync(x => x.ItemStore == maxItemStore && x.Id == tenantId);

        }
        public async Task UpdateAsync(Establishment data)
        {
        }
        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<EstablishmentTenant>> GetAllIEstablishmentByTenantaAsync(string tenantid)
        {
            return await _context.EstablishmentTenants
                 .Where(x => x.Id == tenantid)
                 .ToListAsync();
        }
        public async Task<IList<EstablishmentTenant>> GetAllIEstablishmentByTenantaByIdEstablishmentAsync(string Establsimentid)
        {
            var establishmentTenant = await _context.EstablishmentTenants
             .FirstOrDefaultAsync(x => x.EstablishmentId == Establsimentid && x.IsActive==true);

            if (establishmentTenant == null)
                return new List<EstablishmentTenant>();

            //var tenantId = establishmentTenant.TenantId;

            return await _context.EstablishmentTenants.Where(x => x.Id == establishmentTenant.Id).ToListAsync();
        }
        public async Task<IList<Establishment>> GetAllEstablishmentAsync()
        {
            return await _context.Establishments.ToListAsync();
        }
        public async Task<IList<EstablishmentTenant>> GetAllEstablishmentTeantAsync()
        {
            return await _context.EstablishmentTenants.ToListAsync();
        }
        // public async Task<(List<Establishment> items, int totalitems)> GetAllEstablishmentAsync(int pageindex, int pagesize)
        //{
        //    var query = _context.Establishments.AsQueryable();
        //    var totalitems = await query.CountAsync();

        //    var items = await query
        //    .Skip((pageindex - 1) * pagesize)
        //    .Take(pagesize)
        //        .ToListAsync();

        //    return (items, totalitems);
        //}
        public async Task<bool> ExistEstablishmentCount()
        {
            var result = await _context.Establishments.AnyAsync();
            return result;
        }
        public async Task<bool> EmailEstablishmentUnic(string email)
        {
            var result = await _context.Establishments.AnyAsync(x => x.Email == email);
            return result;
        }
        public async Task<Establishment> GetByIEstablishAsync(string id)
        {
            var data = await _context.Establishments.Include(u => u.Users).Include(d => d.Documents).FirstOrDefaultAsync(a => a.Id == id);
            if (data == null)
            {
                data = new Establishment();

            }
            return data;
        }
        public async Task<Establishment> GetByIEstablishUnicAsync(string id)
        {
            var data = await _context.Establishments.FirstOrDefaultAsync(a => a.Id == id);
            if (data == null)
            {
                data = new Establishment();

            }
            return data;
        }
        public async Task<int> CountEstablishmentsByTenantAsync(string tenantId)
        {
            return await _context.EstablishmentTenants
                .Where(x => x.Id == tenantId)
                .CountAsync();
        }

        #region Document
        public async Task<Document> SaveDocumentAsync(Document data)
        {
            _context.Entry(data).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return data;
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

        ///verificar se tem mais de um stablishment por user
        ///verificar se tem contact por establishment
        ///verifcar se tem document por establishment
        ///verificar se user,document,contact pertence ao establishment e atuvos
        ///validar campos obrigatorios como email
        ///
    }
}
