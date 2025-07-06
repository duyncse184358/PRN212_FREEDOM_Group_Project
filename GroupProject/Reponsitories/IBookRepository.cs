using BusinessObject;
using System.Collections.Generic;

namespace Reponsitories
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book? GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
        List<Book> Search(string keyword);
    }
}