using System;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IUserDataRepository
    {
        UserData GetById(string id);

        //todo
        //Tuple<int bookID, int page> GetByIdUserLastTypedPages(string userID);

        void UpdateById(UserData model);

        void SaveChanges();
        Task SaveAsync();
    }
}
