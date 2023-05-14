using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        int Add(T entity);
        T GetById(int id, params Expression<Func<T, object>>[] includeProperties);
        T Get(int id);
        bool Update(T entity);
        bool Delete(int id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includeProperties = "");
    }
}
