using ListatTestProject_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ListatTestProject_DAL.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllByPredicate(Expression<Func<Sale, bool>> predicate);
        Task<Sale> GetById(int id);
        Task<IEnumerable<Sale>> GetAll();
    }
}
