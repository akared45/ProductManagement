using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.DomainLayer.Interfaces;
using Advanced.InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace Advanced.InfrastructureLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _entities = applicationDbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
