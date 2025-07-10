using BusinessObject;
using Reponsitories;
using Services;
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer.DAO;
using DataAccessLayer;

namespace Services.Implementations
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _repo;
        private readonly IFineService _fineService;
        private readonly BorrowingDAO _borrowingDAO;
        private readonly BookCopyDAO _bookCopyDAO;

        public BorrowingService(IBorrowingRepository repo, IFineService fineService, LibraryDbContext context)
        {
            _repo = repo;
            _fineService = fineService;

            _borrowingDAO = new BorrowingDAO(context);
            _bookCopyDAO = new BookCopyDAO(context);
        }

        public List<Borrowing> GetAllBorrowings() => _repo.GetAll();
        public Borrowing? GetBorrowingById(int id) => _repo.GetById(id);
        public List<Borrowing> GetBorrowingsByPatron(int patronId) => _repo.GetByPatronId(patronId);
        public void BorrowBook(Borrowing borrowing) => _repo.Add(borrowing);
        public void ReturnBook(int borrowingId)
        {
            var borrowing = _repo.GetById(borrowingId);
            if (borrowing != null)
            {
                borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                borrowing.IsReturned = true;
                borrowing.Status = "Returned";
                _repo.Update(borrowing);

                decimal fineAmount = CalculateFine(borrowing);
                if (fineAmount > 0)
                {
                    var newFine = new BusinessObject.Fine
                    {
                        BorrowingId = borrowing.BorrowingId,
                        PatronId = borrowing.PatronId ?? 0,
                        FineAmount = fineAmount,
                        Paid = false,
                        FineDate = DateTime.Now
                    };
                    _fineService.AddFine(newFine);
                }
            }
        }
        public List<Borrowing> GetOverdueBorrowings() =>
            _repo.GetAll().Where(b => (b.Status == "Borrowed" || b.Status == "Overdue")
                                   && (b.IsReturned == null || b.IsReturned == false)
                                   && b.DueDate < DateOnly.FromDateTime(DateTime.Now)).ToList();

        public bool IsOverdue(Borrowing borrowing)
        {
            return (borrowing.Status == "Borrowed" || borrowing.Status == "Overdue")
                   && (borrowing.IsReturned == null || borrowing.IsReturned == false)
                   && borrowing.DueDate < DateOnly.FromDateTime(DateTime.Now);
        }

        public decimal CalculateFine(Borrowing borrowing)
        {
            // ĐÃ SỬA LỖI: Bỏ .Date cho DateOnly
            if ((borrowing.Status != "Returned" && !IsOverdue(borrowing)) || !borrowing.ReturnDate.HasValue) return 0M;

            // Chuyển đổi DateOnly sang DateTime để tính TimeSpan
            DateTime actualReturnDateTime = borrowing.ReturnDate.Value.ToDateTime(TimeOnly.MinValue);
            DateTime dueDateTime = borrowing.DueDate.ToDateTime(TimeOnly.MinValue);

            // ĐÃ SỬA LỖI: Bỏ .Date cho DateTime (vì đã chuyển sang DateTime)
            if (actualReturnDateTime > dueDateTime) // So sánh trực tiếp DateTime
            {
                TimeSpan daysOverdue = actualReturnDateTime - dueDateTime;
                decimal fineRatePerDay = 0.5M;
                return daysOverdue.Days * fineRatePerDay;
            }
            return 0M;
        }

      public void MarkBookCopyAsLost(int borrowingId)
        {
            // Thay GetBorrowingById thành GetById
            var borrowing = _borrowingDAO.GetById(borrowingId);
            if (borrowing == null) throw new Exception("Borrowing record not found.");

            borrowing.Status = "Lost";
            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);

            // Thay UpdateBorrowing thành Update
            _borrowingDAO.Update(borrowing);

            _bookCopyDAO.UpdateFirstAvailableCopyStatusByBookId(borrowing.BookId ?? 0, "Lost");
        }

        public void MarkBookCopyAsDamaged(int borrowingId)
        {
            var borrowing = _borrowingDAO.GetById(borrowingId);
            if (borrowing == null) throw new Exception("Borrowing record not found.");

            borrowing.Status = "Damaged";
            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            _borrowingDAO.Update(borrowing);

            _bookCopyDAO.UpdateFirstAvailableCopyStatusByBookId(borrowing.BookId ?? 0, "Damaged");
        }


    }
}