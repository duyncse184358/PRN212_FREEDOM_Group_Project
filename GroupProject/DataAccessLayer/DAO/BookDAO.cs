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
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
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

        public List<Book> Search(string keyword) =>
            _context.Books.Include(b => b.Category)
                   .Where(b => (b.Title != null && b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                                (b.Author != null && b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                                (b.ShelfLocation != null && b.ShelfLocation.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                                (b.Category != null && b.Category.CategoryName.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                   .ToList();
    }
}