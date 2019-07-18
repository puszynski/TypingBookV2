using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IUserDataRepository
    {
        UserData GetById(string id);

        void UpdateById(UserData model);

        void SaveChanges();
        Task SaveAsync();
    }
}
