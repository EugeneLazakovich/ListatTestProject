using ListatTestProject_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ListatTestProject_DAL.Interfaces
{
    public interface IGenericRepository<T>
        where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> DeleteById(int id);
        Task<bool> Update(T item);
        Task<int> Add(T item);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByPredicate(Expression<Func<T, bool>> predicate);
    }
}
