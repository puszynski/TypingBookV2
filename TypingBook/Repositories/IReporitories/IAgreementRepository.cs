using System.Linq;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IAgreementRepository
    {
        IQueryable<Agreement> GetAllAgreements();
    }
}
