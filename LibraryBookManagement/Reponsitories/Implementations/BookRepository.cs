using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccessLayer.DAO;
using DataAccessLayer;

namespace Reponsitories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDAO dao;
        public BookRepository(LibraryDbContext context) => dao = new BookDAO(context);
        public List<Book> GetAll() => dao.GetAll();
        public Book GetById(int id) => dao.GetById(id);
        public void Add(Book book) => dao.Add(book);
        public void Update(Book book) => dao.Update(book);
        public void Delete(int id) => dao.Delete(id);
    }
}
