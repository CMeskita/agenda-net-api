using AgendaNet_Domain.Entities;
namespace AgendaNet_Domain.Interfaces
{
    public interface IEstablishmentRepository
    {
        Task SaveAsync(Establishment data);
        Task UpdateAsync(Establishment data);
        Task<bool> DeleteAsync(string id);
        Task<(List<Establishment> items, int totalitems)> GetAllAsync(int pageindex, int pagesize);

        #region Document
        Task SaveDocumentAsync(Document data);
        Task UpdateDocumentAsync(Document data);
        Task<bool> DeleteDocumentAsync(string id);
        Task<(List<Document> items, int totalitems)> GetAllDocumentAsync(int pageindex, int pagesize);
        #endregion
    }
}
