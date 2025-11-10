using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Domain.Interfaces
{
    public interface IUnitofWork: IDisposable
    {
    
        void BeginTransaction();
        void CommitTransaction();
        Task<int> CommitAsync();
        void Rollback();
    }
}
