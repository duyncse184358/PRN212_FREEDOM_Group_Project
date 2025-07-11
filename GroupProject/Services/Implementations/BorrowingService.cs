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
        private readonly BookDAO _bookDAO;

        public BorrowingService(IBorrowingRepository repo, IFineService fineService, LibraryDbContext context, BookDAO bookDAO)
        {
            _repo = repo;
            _fineService = fineService;

            _borrowingDAO = new BorrowingDAO(context);
            _bookCopyDAO = new BookCopyDAO(context);
            _bookDAO = bookDAO;
        }

        public List<Borrowing> GetAllBorrowings() => _repo.GetAll();
        public Borrowing? GetBorrowingById(int id) => _repo.GetById(id);
        public List<Borrowing> GetBorrowingsByPatron(int patronId) => _repo.GetByPatronId(patronId);
        public void BorrowBook(int bookId, int patronId, DateOnly borrowDate, DateOnly dueDate)
        {
            var availableCopy = _bookCopyDAO.GetAvailableCopyByBookId(bookId);
            if (availableCopy == null)
                throw new Exception("Không còn bản copy nào khả dụng!");

            availableCopy.Status = "Borrowed";
            _bookCopyDAO.Update(availableCopy);

            // Lấy Book từ BookDAO/BookRepository để cập nhật số lượng
            var book = _bookDAO.GetById(bookId);
            if (book == null || book.AvailableCopies <= 0)
                throw new Exception("Sách không còn bản nào khả dụng!");
            book.AvailableCopies--;
            if (book.AvailableCopies == 0) book.Status = "Borrowed";
            _bookDAO.Update(book);

            var borrowing = new Borrowing
            {
                BookId = bookId,
                PatronId = patronId,
                BorrowDate = borrowDate,
                DueDate = dueDate,
                Status = "Borrowed",
                IsReturned = false,
                CopyId = availableCopy.CopyId
            };
            _borrowingDAO.Add(borrowing);
        }



        // Sửa ReturnBook
        public void ReturnBook(int borrowingId)
        {
            var borrowing = _repo.GetById(borrowingId);
            if (borrowing != null)
            {
                borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                borrowing.IsReturned = true;
                borrowing.Status = "Returned";
                _repo.Update(borrowing);

                // Lấy Book để cộng lại số sách còn lại
                var book = _bookDAO.GetById(borrowing.BookId ?? 0);
                if (book != null)
                {
                    book.AvailableCopies++;
                    if (book.Status == "Borrowed" && book.AvailableCopies > 0)
                        book.Status = "Available";
                    _bookDAO.Update(book);
                }

                // Cập nhật trạng thái BookCopy
                if (borrowing.CopyId.HasValue)
                {
                    var copy = _bookCopyDAO.GetById(borrowing.CopyId.Value);
                    if (copy != null)
                    {
                        copy.Status = "Available";
                        _bookCopyDAO.Update(copy);
                    }
                }

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
            var borrowing = _borrowingDAO.GetById(borrowingId);
            if (borrowing == null) throw new Exception("Không tìm thấy phiếu mượn.");

            if (borrowing.CopyId.HasValue)
            {
                var copy = _bookCopyDAO.GetById(borrowing.CopyId.Value);
                if (copy != null && copy.Status != "Lost")
                {
                    // CHỈ thực hiện giảm số lượng nếu bản copy đang là "Available"
                    if (copy.Status == "Available")
                    {
                        var book = _bookDAO.GetById(copy.BookId);
                        if (book != null && book.AvailableCopies > 0)
                        {
                            book.AvailableCopies--;
                            _bookDAO.Update(book);
                        }
                    }
                    copy.Status = "Lost";
                    _bookCopyDAO.Update(copy);

                    // Cập nhật trạng thái Borrowing
                    borrowing.Status = "Lost";
                    borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                    _borrowingDAO.Update(borrowing);
                }
            }
        }
        public void MarkBookCopyAsDamaged(int borrowingId)
        {
            var borrowing = _borrowingDAO.GetById(borrowingId);
            if (borrowing == null) throw new Exception("Borrowing record not found.");

            borrowing.Status = "Damaged";
            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            _borrowingDAO.Update(borrowing);

            if (borrowing.CopyId.HasValue)
                _bookCopyDAO.UpdateStatusByCopyId(borrowing.CopyId.Value, "Damaged");
        }
        public void MarkBookCopyAsNormal(int borrowingId)
        {
            var borrowing = _borrowingDAO.GetById(borrowingId);
            if (borrowing == null) throw new Exception("Không tìm thấy phiếu mượn.");

            if (borrowing.CopyId.HasValue)
            {
                var copy = _bookCopyDAO.GetById(borrowing.CopyId.Value);
                if (copy != null && copy.Status == "Lost")
                {
                    // Nếu phiếu mượn đã trả rồi thì cộng lại số lượng
                    if (borrowing.Status == "Returned")
                    {
                        var book = _bookDAO.GetById(copy.BookId);
                        if (book != null)
                        {
                            book.AvailableCopies++;
                            if (book.Status == "Borrowed" && book.AvailableCopies > 0)
                                book.Status = "Available";
                            _bookDAO.Update(book);
                        }
                        // Trạng thái phiếu mượn vẫn giữ là "Returned"
                    }
                    else if (borrowing.Status == "Lost")
                    {
                        // Trường hợp này: phiếu mượn chưa trả, chỉ chuyển lại trạng thái về Borrowed
                        borrowing.Status = "Borrowed";
                        borrowing.ReturnDate = null;        // <-- Xóa ReturnDate
                        borrowing.IsReturned = false;       // <-- Đánh dấu chưa trả
                        _borrowingDAO.Update(borrowing);
                        _repo.Update(borrowing); // Nếu UI lấy từ repo
                    }
                    // Luôn phục hồi trạng thái copy
                    copy.Status = "Available";
                    _bookCopyDAO.Update(copy);
                }
            }
        }


    }
}