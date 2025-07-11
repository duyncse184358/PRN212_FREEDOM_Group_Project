using BusinessObject;
using Reponsitories;
using Services;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IBorrowingRepository _borrowingRepo;

        public BookService(IBookRepository bookRepo, IBorrowingRepository borrowingRepo)
        {
            _bookRepo = bookRepo;
            _borrowingRepo = borrowingRepo;
        }

        public List<Book> GetAllBooks() => _bookRepo.GetAll();
        public Book? GetBookById(int id) => _bookRepo.GetById(id);
        public void AddBook(Book book) => _bookRepo.Add(book);
        public void UpdateBook(Book book) => _bookRepo.Update(book);
        public void DeleteBook(int id) => _bookRepo.Delete(id);
        public List<Book> SearchBooks(string keyword) => _bookRepo.Search(keyword);

        public void BorrowBook(Borrowing newBorrowing)
        {
            var book = _bookRepo.GetById(newBorrowing.BookId ?? 0);
            if (book == null || book.AvailableCopies <= 0 || book.Status != "Available")
            {
                throw new InvalidOperationException("Book is not available for borrowing.");
            }

            _borrowingRepo.Add(newBorrowing);

            book.AvailableCopies--;
            if (book.AvailableCopies == 0) book.Status = "Borrowed";
            _bookRepo.Update(book);
        }

        public void ReturnBook(int borrowingId)
        {
            var borrowing = _borrowingRepo.GetById(borrowingId);
            if (borrowing == null || borrowing.Status != "Borrowed")
            {
                throw new InvalidOperationException("This book is not currently borrowed or already returned.");
            }

            borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            borrowing.Status = "Returned";
            borrowing.IsReturned = true;
            _borrowingRepo.Update(borrowing);

            var book = _bookRepo.GetById(borrowing.BookId ?? 0);
            if (book != null)
            {
                book.AvailableCopies++;
                if (book.Status == "Borrowed" && book.AvailableCopies > 0) book.Status = "Available";
                _bookRepo.Update(book);
            }
        }
        public void MarkBookStatus(int bookId, string status)
        {
            var book = _bookRepo.GetById(bookId);
            if (book != null)
            {
                book.Status = status;

                // Nếu mất sách thì trừ AvailableCopies
                if (status == "Lost" && book.AvailableCopies > 0)
                {
                    book.AvailableCopies--;
                }
                _bookRepo.Update(book); // ✅ dùng repository để lưu
            }
        }
        public void AddBookWithCopies(Book book)
        {
            _bookRepo.AddBookWithCopies(book);
        }

    }
}