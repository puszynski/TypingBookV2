using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IUserDataRepository
    {
        UserData GetById(string userId);
        List<(int bookID, int userLastPage)> GetByIdUserLastTypedPages(string userId);
        void UpdateById(UserData model);
        string GetStatisticsByUserId(string userId);

        void UpateStatisticsByUserId(string userId, string statistics);

        void SaveChanges();
        Task SaveAsync();
    }
}
