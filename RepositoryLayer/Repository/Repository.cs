using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Context _context;
        readonly DbSet<T> entities;

        public Repository(Context context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            await entities.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            entities.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = entities;
            return query;
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            entities.Update(entity);
        }
    }
}
