using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public interface IDataBase<T> where T : class, new()
    {
        Task<T> Get(int id);

        Task<List<T>> GetAsync<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);

        Task<T> Get(Expression<Func<T, bool>> predicate);

       // AsyncTableQuery<T> AsQueryable();

        Task<int> Insert(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(T entity);

    }
}
