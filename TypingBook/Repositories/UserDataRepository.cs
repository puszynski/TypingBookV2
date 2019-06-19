using System;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Data;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;

namespace TypingBook.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly ApplicationDbContext _db;

        public UserDataRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public UserData GetById(string id)
        {
            return _db.UserData.Single(x => x.UserId == id);
        }

        public void UpdateById(UserData model)
        {
            _db.Update(model);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
