using System.Linq;
using TypingBook.Data;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;

namespace TypingBook.Repositories
{
    public class AgreementRepository : IAgreementRepository
    {
        private readonly ApplicationDbContext _db;

        public AgreementRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Agreement> GetAllAgreements()
        {
            return _db.Agreements;
        }
    }
}
