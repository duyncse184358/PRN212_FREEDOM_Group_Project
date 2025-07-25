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
            var existingBook = _context.Books
                .Include(b => b.BookCopies)
                .FirstOrDefault(b => b.BookId == book.BookId);

            if (existingBook != null)
            {
                // Cập nhật thông tin chung
                existingBook.Title = book.Title;
                existingBook.Isbn = book.Isbn;
                existingBook.CategoryId = book.CategoryId;
                existingBook.Author = book.Author;
                existingBook.PublicationYear = book.PublicationYear;
                existingBook.Publisher = book.Publisher;
                existingBook.Genre = book.Genre;
                existingBook.ShelfLocation = book.ShelfLocation;
                existingBook.Status = book.Status;
                existingBook.Price = book.Price;
                existingBook.ImagePath = book.ImagePath;

                int currentAvailableCopies = existingBook.BookCopies.Count(c => c.Status == "Available");
                int currentTotalCopies = existingBook.BookCopies.Count;
                int desiredCopies = (int)book.AvailableCopies;

                if (currentAvailableCopies < desiredCopies)
                {
                    int toAdd = desiredCopies - currentAvailableCopies;

                    for (int i = 0; i < toAdd; i++)
                    {
                        _context.BookCopies.Add(new BookCopy
                        {
                            BookId = book.BookId,
                            Status = "Available"
                        });
                    }

                    _context.SaveChanges(); // Cập nhật BookCopies trước
                }
                else if (currentAvailableCopies > desiredCopies)
                {
                    int toRemove = currentAvailableCopies - desiredCopies;

                    var availableCopies = existingBook.BookCopies
                        .Where(c => c.Status == "Available")
                        .Take(toRemove)
                        .ToList();

                    if (availableCopies.Count < toRemove)
                    {
                        throw new InvalidOperationException("Không thể giảm bản sao vì có sách đang được mượn.");
                    }

                    _context.BookCopies.RemoveRange(availableCopies);
                    _context.SaveChanges(); // Xoá xong BookCopies
                }

                // Cập nhật lại số lượng bản sao hiện tại
                int updatedTotalCopies = _context.BookCopies.Count(c => c.BookId == book.BookId);
                int updatedAvailableCopies = _context.BookCopies.Count(c => c.BookId == book.BookId && c.Status == "Available");

                existingBook.NumberOfCopies = updatedTotalCopies;
                existingBook.AvailableCopies = updatedAvailableCopies;

                _context.SaveChanges();
            }
        }




        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                // Xóa các bản sao (copies) của sách trước, nếu có ràng buộc
                var relatedCopies = _context.BookCopies.Where(c => c.BookId == id).ToList();
                if (relatedCopies.Any())
                {
                    _context.BookCopies.RemoveRange(relatedCopies);
                }

                // Xóa sách
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



        public List<Book> Search(string keyword)
        {
            keyword = keyword?.ToLower() ?? "";
            return _context.Books.Include(b => b.Category)
                .Where(b =>
                    (b.Title != null && b.Title.ToLower().Contains(keyword)) ||
                    (b.Author != null && b.Author.ToLower().Contains(keyword)) ||
                    (b.ShelfLocation != null && b.ShelfLocation.ToLower().Contains(keyword)) ||
                    (b.Category != null && b.Category.CategoryName.ToLower().Contains(keyword))
                )
                .ToList();
        }


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
