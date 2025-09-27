using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.General;
using TechnicalTask.BusinessLogic.Interfaces.IRepositories;
using TechnicalTask.DataAccess.Data;

namespace TechnicalTask.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll() => _dbSet.Where(e => !e.IsDeleted);
        public  T GetById(int id) =>  _dbSet.SingleOrDefault(e => e.Id == id && !e.IsDeleted);
        public  T Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            return _dbSet.Add(entity).Entity;
        }

        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedAt = DateTime.Now;
            return entity;
        }
        public T Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if (entity == null)
            {
                return null;
            }
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            return entity;
        }



    }
}
