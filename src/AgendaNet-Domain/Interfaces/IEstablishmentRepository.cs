using AgendaNet_Domain.Entities;
using System.Threading.Tasks;
namespace AgendaNet_Domain.Interfaces
{
    public interface IEstablishmentRepository
    {
        Task<Establishment> SaveAsync(Establishment data);
        Task<EstablishmentTenant> TenantSaveAsync(EstablishmentTenant data);
        Task<IList<EstablishmentTenant>> GetAllIEstablishmentByTenantaAsync(string id);
        Task<IList<Establishment>> GetAllEstablishmentAsync();
        Task<EstablishmentTenant> GetEstablishmentTenantMaxAsync(string tenantid);
        Task<Establishment> GetByIEstablishUnicAsync(string id);
        Task<IList<EstablishmentTenant>> GetAllEstablishmentTeantAsync();
        Task<IList<EstablishmentTenant>> GetAllIEstablishmentByTenantaByIdEstablishmentAsync(string Establsimentid);
        Task<int> CountEstablishmentsByTenantAsync(string tenantId);
        Task UpdateAsync(Establishment data);
        Task<bool> DeleteAsync(string id);
       // Task<(List<Establishment> items, int totalitems)> GetAllEstablishmentAsync(int pageindex, int pagesize);
        Task<bool> ExistEstablishmentCount();
        Task<Establishment> GetByIEstablishAsync(string id);

        #region Document
        Task<Document> SaveDocumentAsync(Document data);
        Task UpdateDocumentAsync(Document data);
        Task<bool> DeleteDocumentAsync(string id);
        Task<(List<Document> items, int totalitems)> GetAllDocumentAsync(int pageindex, int pagesize);
        Task<bool> EmailEstablishmentUnic(string email);
        #endregion
    }
}
