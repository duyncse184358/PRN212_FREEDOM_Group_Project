using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class FineDAO
    {
        private readonly LibraryDbContext _context;

        public FineDAO(LibraryDbContext context) => _context = context;

        public List<Fine> GetByPatronId(int patronId) =>
            _context.Fines.Where(f => f.Borrowing.PatronId == patronId).ToList();

        public void Add(Fine f)
        {
            _context.Fines.Add(f);
            _context.SaveChanges();
        }

        public void MarkAsPaid(int fineId)
        {
            var fine = _context.Fines.Find(fineId);
            if (fine != null)
            {
                fine.Paid = true;
                _context.SaveChanges();
            }
        }
    }
}
