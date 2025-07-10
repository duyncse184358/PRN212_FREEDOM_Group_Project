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
            if (borrowing == null)
                throw new ArgumentNullException(nameof(borrowing), "Borrowing object cannot be null.");

            try
            {
                _context.Borrowings.Add(borrowing);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ra Output Window để dễ debug
                System.Diagnostics.Debug.WriteLine($"[BorrowingDAO] Error when adding borrowing: {ex.Message}");
                throw; // Ném lại exception để tầng trên xử lý
            }
        }

        public void Update(Borrowing borrowing)
        {
            if (borrowing == null)
                throw new ArgumentNullException(nameof(borrowing), "Borrowing object cannot be null.");

            try
            {
                _context.Borrowings.Update(borrowing);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingDAO] Error when updating borrowing: {ex.Message}");
                throw;
            }
        }

        public void Delete(int id)
        {
            var borrowing = _context.Borrowings.Find(id);
            if (borrowing != null)
            {
                try
                {
                    _context.Borrowings.Remove(borrowing);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[BorrowingDAO] Error when deleting borrowing: {ex.Message}");
                    throw;
                }
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
