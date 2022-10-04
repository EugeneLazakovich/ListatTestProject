using ListatTestProject_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ListatTestProject_DAL.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllByPredicate(string name, MarketStatus? status, string seller, int limit, int page);
        Task<Sale> GetById(int id);
        Task<IEnumerable<Sale>> GetAll();
    }
}
