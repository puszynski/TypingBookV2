using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Data;
using TypingBook.Helpers;
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


        public UserData GetById(string userID)
        {
            var result = _db.UserData.SingleOrDefault(x => x.UserId == userID);

            if (result == null)
                return Create(userID);

            return result;
        }

        public List<(int bookID, int userLastPage)> GetByIdUserLastTypedPages(string userID)
        {
            var result = _db.UserData.SingleOrDefault(x => x.UserId == userID).BookProgress;
            
            var userDataHelper = new UserDataHelper();
            return userDataHelper.GetAllBooksCurrentPage(result);
        }

        public string GetStatisticsByUserId(string userId) => _db.UserData.SingleOrDefault(x => x.UserId == userId).Statistics;
        public void UpateStatisticsByUserId(string userId, string statistics)
        {
            var model = GetById(userId);
            model.Statistics = statistics;
            UpdateById(model);
        }
        
        public void UpdateById(UserData model) => _db.Update(model);


        public async Task SaveAsync() => await _db.SaveChangesAsync();

        public void SaveChanges() => _db.SaveChanges();

        private UserData Create(string id)
        {
            var newUserData = new UserData() { UserId = id };
            _db.Add(newUserData);
            SaveChanges();
            return newUserData;
        }
    }
}
