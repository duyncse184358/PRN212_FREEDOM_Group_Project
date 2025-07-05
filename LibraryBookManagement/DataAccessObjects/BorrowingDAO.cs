using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class BorrowingDAO
    {
        private readonly LibraryDbContext _context;

        public BorrowingDAO(LibraryDbContext context) => _context = context;

        public List<Borrowing> GetAll() =>
            _context.Borrowings.Include(b => b.Book).Include(b => b.Patron).ToList();

        public List<Borrowing> GetByPatronId(int patronId) =>
            _context.Borrowings.Where(b => b.PatronId == patronId).ToList();

        public void Add(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }

        public void MarkReturned(int borrowingId)
        {
            var borrow = _context.Borrowings.Find(borrowingId);
            if (borrow != null)
            {
                borrow.IsReturned = true;
                _context.SaveChanges();
            }
        }
    }
}
