using System.Collections.Generic;
using BusinessObject;

namespace Services
{
    public interface IBookCopyService
    {
        List<BookCopy> GetByBookId(int bookId);
        BookCopy? GetById(int copyId);
        void UpdateStatus(int copyId, string newStatus);
        void Add(BookCopy copy);
        void Delete(int copyId);
        void UpdateStatusByBookId(int bookId, string newStatus);

    }
}
