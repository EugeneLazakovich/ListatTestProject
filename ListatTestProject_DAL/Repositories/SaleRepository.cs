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
    public class SaleRepository : ISaleRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public SaleRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sale>> GetAll()
        {
            return await _dbContext.Sales.Include(s => s.Item).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllByPredicate(Expression<Func<Sale, bool>> predicate)
        {
            return await _dbContext.Sales
                .Include(s => s.Item)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Sale> GetById(int id)
        {
            return await _dbContext.Sales.Include(s => s.Item).Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
