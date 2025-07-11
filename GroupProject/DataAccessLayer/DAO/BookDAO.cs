    using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DataAccessLayer.DAO
{
    public class BookDAO
    {
        private readonly LibraryDbContext _context;

        public BookDAO(LibraryDbContext context) => _context = context;

        public List<Book> GetAll() => _context.Books.Include(b => b.Category).ToList();

        public Book? GetById(int id) => _context.Books.Include(b => b.Category).FirstOrDefault(b => b.BookId == id);

        public void Add(Book book)
        {
            _context.Books.Add(book); // EF sẽ tự nhận thuộc tính Price
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            var trackedBook = _context.Books.FirstOrDefault(b => b.BookId == book.BookId);
            if (trackedBook != null)
            {
                trackedBook.Isbn = book.Isbn;
                trackedBook.Title = book.Title;
                trackedBook.Author = book.Author;
                trackedBook.Publisher = book.Publisher;
                trackedBook.PublicationYear = book.PublicationYear;
                trackedBook.Genre = book.Genre;
                trackedBook.NumberOfCopies = book.NumberOfCopies;
                trackedBook.AvailableCopies = book.AvailableCopies;
                trackedBook.ShelfLocation = book.ShelfLocation;
                trackedBook.CategoryId = book.CategoryId;
                trackedBook.Status = book.Status;
                trackedBook.Price = book.Price; // 👈 Thêm dòng này

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
        public void AddBookWithCopies(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            int bookId = book.BookId;
            for (int i = 0; i < book.NumberOfCopies; i++)
            {
                var copy = new BookCopy { BookId = bookId, Status = "Available" };
                _context.BookCopies.Add(copy);
            }
            _context.SaveChanges();
        }



        public List<Book> Search(string keyword) =>
            _context.Books.Include(b => b.Category)
                   .Where(b => (b.Title != null && b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                                (b.Author != null && b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                                (b.ShelfLocation != null && b.ShelfLocation.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                                (b.Category != null && b.Category.CategoryName.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                   .ToList();


        public void BorrowBookWithCopy(int bookId, int patronId, DateOnly borrowDate, DateOnly dueDate)
        {
            // 1. Tìm 1 BookCopy còn Available
            var availableCopy = _context.BookCopies.FirstOrDefault(c => c.BookId == bookId && c.Status == "Available");
            if (availableCopy == null)
                throw new Exception("Không còn bản copy nào khả dụng!");

            // 2. Cập nhật trạng thái BookCopy
            availableCopy.Status = "Borrowed";

            // 3. Giảm số AvailableCopies ở Book
            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book != null && book.AvailableCopies > 0)
            {
                book.AvailableCopies--;
                if (book.AvailableCopies == 0) book.Status = "Borrowed";
            }

            // 4. Tạo mới Borrowing với CopyID
            var borrowing = new Borrowing
            {
                BookId = bookId,
                PatronId = patronId,
                BorrowDate = borrowDate,
                DueDate = dueDate,
                Status = "Borrowed",
                IsReturned = false,
                CopyId = availableCopy.CopyId, // <-- Gắn bản copy
            };
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }


    }




}
