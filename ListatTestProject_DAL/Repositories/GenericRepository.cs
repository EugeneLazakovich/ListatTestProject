using ListatTestProject_DAL.Interfaces;
using ListatTestProject_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ListatTestProject_DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> 
        where T : BaseEntity, new()
    {
        private readonly EFCoreDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<int> Add(T item)
        {
            //item.Id = Guid.NewGuid();
            _dbSet.Add(item);
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<bool> DeleteById(int id)
        {
            var item = new T { Id = id };
            _dbContext.Entry(item).State = EntityState.Deleted;

            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<T> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
