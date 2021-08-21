using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntiqueAuction.Shared.Domain;

// Base interface of CRUD operations which is shared by all the repository interfaces
namespace AntiqueAuction.Core.Repository
{
    public interface IRepository<T> where T:Entity
    {
        IEnumerable<T> Get();  
        Task<T> Get(Guid id);
        Task<T> Insert(T entity);  
        Task Update(T entity);
        Task Update(IEnumerable<T> entity);
        Task Delete(Guid id);  
    }
}  