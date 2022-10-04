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

        public async Task<IEnumerable<Sale>> GetAllByPredicate(string name, 
            MarketStatus? status, 
            string seller, 
            int limit, 
            int page)
        {
            bool isNullOrEmptyName = string.IsNullOrEmpty(name);
            bool hasValueStatus = status.HasValue;
            bool isNullOrEmptySeller = string.IsNullOrEmpty(seller);
            return await _dbContext.Sales
                .Include(s => s.Item)
                .Where(s =>
                    (isNullOrEmptyName || s.Item.Name.ToLower().Contains(name.ToLower())) &&
                    (!hasValueStatus || status.Value == s.Status) &&
                    (isNullOrEmptySeller || s.Seller.ToLower().Contains(seller.ToLower())))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Sale> GetById(int id)
        {
            return await _dbContext.Sales.Include(s => s.Item).Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
