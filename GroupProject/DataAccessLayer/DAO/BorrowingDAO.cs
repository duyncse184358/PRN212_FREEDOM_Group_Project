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
        public void MarkLostAndFine(int borrowingId)
        {
            var borrowing = _context.Borrowings.Include(b => b.Book).FirstOrDefault(b => b.BorrowingId == borrowingId);
            if (borrowing == null) return;

            // Đánh dấu trạng thái
            borrowing.Status = "Lost";
            borrowing.IsReturned = false;

            // Kiểm tra đã có khoản phạt chưa
            var existingFine = _context.Fines.FirstOrDefault(f =>
                f.BorrowingId == borrowingId && f.PatronId == borrowing.PatronId && (f.Paid == null || f.Paid == false));
            if (existingFine == null && borrowing.Book != null)
            {
                var fine = new Fine
                {
                    BorrowingId = borrowingId,
                    PatronId = borrowing.PatronId,
                    FineAmount = borrowing.Book.Price,
                    Paid = false,
                    FineDate = DateTime.Now
                };
                _context.Fines.Add(fine);
            }

            _context.SaveChanges();
        }

        public void MarkDamagedAndFine(int borrowingId)
        {
            var borrowing = _context.Borrowings.Include(b => b.Book).FirstOrDefault(b => b.BorrowingId == borrowingId);
            if (borrowing == null) return;

            borrowing.Status = "Damaged";
            borrowing.IsReturned = false;

            var existingFine = _context.Fines.FirstOrDefault(f =>
                f.BorrowingId == borrowingId && f.PatronId == borrowing.PatronId && (f.Paid == null || f.Paid == false));
            if (existingFine == null && borrowing.Book != null)
            {
                var fine = new Fine
                {
                    BorrowingId = borrowingId,
                    PatronId = borrowing.PatronId,
                    FineAmount = borrowing.Book.Price,
                    Paid = false,
                    FineDate = DateTime.Now
                };
                _context.Fines.Add(fine);
            }

            _context.SaveChanges();
        }

    }
}
