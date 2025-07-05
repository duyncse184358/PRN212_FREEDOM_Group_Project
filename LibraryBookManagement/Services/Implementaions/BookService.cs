using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;
        public BookService(IBookRepository repository) => this.repository = repository;

        public List<Book> GetAllBooks() => repository.GetAll();

        public Book GetBookById(int id)
        {
            var book = repository.GetById(id);
            if (book == null) throw new Exception("Book not found");
            return book;
        }

        public void AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title)) throw new ArgumentException("Book title is required");
            repository.Add(book);
        }

        public void UpdateBook(Book book) => repository.Update(book);
        public void DeleteBook(int id) => repository.Delete(id);
    }

}
