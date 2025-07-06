using BusinessObject;
using DataAccessLayer.DAO;
using System.Collections.Generic;

namespace Reponsitories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDAO _bookDao;

        public BookRepository(BookDAO bookDao)
        {
            _bookDao = bookDao;
        }

        public List<Book> GetAll() => _bookDao.GetAll();
        public Book? GetById(int id) => _bookDao.GetById(id);
        public void Add(Book book) => _bookDao.Add(book);
        public void Update(Book book) => _bookDao.Update(book);
        public void Delete(int id) => _bookDao.Delete(id);
        public List<Book> Search(string keyword) => _bookDao.Search(keyword);
    }
}