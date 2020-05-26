using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IUserDataRepository
    {
        UserData GetById(string id);

        List<(int bookID, int userLastPage)> GetByIdUserLastTypedPages(string userID);

        void UpdateById(UserData model);

        void SaveChanges();
        Task SaveAsync();
    }
}
