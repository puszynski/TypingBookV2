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
            var result = _db.UserData.SingleOrDefault(x => x.UserId == id);

            if (result == null)
                return Create(id);

            return result;
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

        private UserData Create(string id)
        {
            var newUserData = new UserData() { UserId = id };
            _db.Add(newUserData);
            SaveChanges();
            return newUserData;
        }
    }
}
