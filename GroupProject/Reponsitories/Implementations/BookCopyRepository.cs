using System.Collections.Generic;
using BusinessObject;
using DataAccessLayer.DAO;

namespace Repositories.Implementations
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly BookCopyDAO _dao;

        public BookCopyRepository(BookCopyDAO dao)
        {
            _dao = dao;
        }

        public List<BookCopy> GetByBookId(int bookId) => _dao.GetByBookId(bookId);
        public BookCopy? GetById(int id) => _dao.GetById(id);
        public void UpdateStatus(int copyId, string newStatus) => _dao.UpdateStatus(copyId, newStatus);
        public void Add(BookCopy copy) => _dao.Add(copy);
        public void Delete(int copyId) => _dao.Delete(copyId);

        public void UpdateStatusByBookId(int bookId, string newStatus)
        {
            _dao.UpdateFirstAvailableCopyStatusByBookId(bookId, newStatus);
        }
    }
}
