using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IGenericRepository<T>where T: class
    {
        IQueryable<T> GetQuery { get; }
        Task<List<T>> GetAll();
        Task<T?> GetById<P>(P id);
        Task<T?> GetById(Guid id, bool track);

        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
