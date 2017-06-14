using Aiesec_App.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public class ItemDataBase
    {
        readonly SQLiteAsyncConnection database;

        public ItemDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }

        public Task<List<Item>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Item>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Item> GetItemAsync(string id)
        {
            return database.Table<Item>().Where(i => i.ID.Equals(id)).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            //if (!string.IsNullOrEmpty(item.ID))
            //{
            //    return database.UpdateAsync(item);
            //}
            //else
            //{
                return database.InsertAsync(item);
          //  }
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return database.DeleteAsync(item);
        }
    }
}
