using BusinessObject;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookCopyRepository
    {
        BookCopy? GetById(int copyId);
        List<BookCopy> GetByBookId(int bookId);
        void UpdateStatus(int copyId, string newStatus);
        void UpdateStatusByBookId(int bookId, string newStatus); // ✅ Thêm dòng này
        void Add(BookCopy copy);
        void Delete(int copyId);
    }
}
