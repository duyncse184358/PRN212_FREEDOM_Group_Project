using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DataAccessLayer.DAO
{
    public class BorrowingDAO
    {
        private readonly LibraryDbContext _context;

        public BorrowingDAO(LibraryDbContext context) => _context = context;

        public List<Borrowing> GetAll() =>
            _context.Borrowings.Include(b => b.Book).Include(b => b.Patron).ToList();

        public Borrowing? GetById(int id) =>
            _context.Borrowings.Include(b => b.Book).Include(b => b.Patron).FirstOrDefault(b => b.BorrowingId == id);


        public List<Borrowing> GetByPatronId(int patronId) =>
            _context.Borrowings.Include(b => b.Book).Include(b => b.Patron)
                      .Where(b => b.PatronId == patronId).ToList();

        public void Add(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }

        public void Update(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var borrowing = _context.Borrowings.Find(id);
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                _context.SaveChanges();
            }
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