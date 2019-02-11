using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Models;
using System.Data.SQLite;
using Dapper;


namespace TypingBook.Data
{
    /// <summary>
    /// Using SQLite DB and Dapper ORM; More about the Dapper(king ): http://dapper-tutorial.net 
    /// Entity Framework can be used too but its slower: https://docs.microsoft.com/pl-pl/ef/core/get-started/netcore/new-db-sqlite
    /// </summary>
    public class SQLiteDapperRepository : ISQLiteDapperRepository
    {
        private readonly string _connectionString = "Data Source=C:/TypingBookV2/TypingBookV2/SQLiteDB.db;Version=3";

        #region get from db    
        public IQueryable<Book> EditContent()
        {
            //ToDo (just example)
            return null;
        }

        public IQueryable<Book> CreateNewBook()
        {
            return null;
        }

        public void Save(IQueryable<Book> booksQuery)
        {
            //ToDo - save all;
        }
        #endregion

        #region get from db
        public IEnumerable<Book> GetAll()
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.Query<Book>("select * from Book", new DynamicParameters());
                return output;
            }
        }

        public Book GetByID(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.QuerySingle<Book>($"select * from Book where ID = {id}", new DynamicParameters());
                return output;
            }
        }

        public async Task<Book> GetByIDAsync(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var output = await cnn.QueryFirstAsync<Book>($"select * from Book where ID = {id}", new DynamicParameters());
                return output;
            }
        }
        #endregion
    }
}
