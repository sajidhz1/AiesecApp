using Aiesec_App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public class Database<T> : IDataBase<T>  where T : class,new()
    {
        readonly SQLiteAsyncConnection database;

        public Database(SQLiteAsyncConnection database)
        {
            this.database = database;
            database.CreateTableAsync<T>().Wait();
        }

        public async Task<T> Get(int id) =>
             await database.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await database.FindAsync<T>(predicate);

        public AsyncTableQuery<T> AsQueryable() => database.Table<T>();

        public async Task<List<T>> Get() =>   await database.Table<T>().ToListAsync();

        public async Task<int> Insert(T entity) =>
             await database.InsertAsync(entity);

        public async Task<int> Update(T entity) =>
             await database.UpdateAsync(entity);

        public async Task<int> Delete(T entity) =>
             await database.DeleteAsync(entity);

        public async Task<List<T>> GetAsync<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = database.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }
    }
}
