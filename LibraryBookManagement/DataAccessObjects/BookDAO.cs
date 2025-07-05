using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class BookDAO
    {
        private readonly LibraryDbContext _context;

        public BookDAO(LibraryDbContext context) => _context = context;

        public List<Book> GetAll() => _context.Books.Include(b => b.Category).ToList();

        public Book GetById(int id) => _context.Books.Include(b => b.Category).FirstOrDefault(b => b.BookId == id);

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
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
