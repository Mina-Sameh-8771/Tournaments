using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    interface IRepository<T>
    {
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetById(int id);
        IQueryable<T> GetAll();
    }
}
