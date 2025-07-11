using System.Collections.Generic;
using System.Linq;
using BusinessObject;

namespace DataAccessLayer.DAO
{
    public class BookCopyDAO
    {
        private readonly LibraryDbContext _context;

        public BookCopyDAO(LibraryDbContext context)
        {
            _context = context; // Sử dụng context từ DI
        }

        public List<BookCopy> GetByBookId(int bookId)
        {
            return _context.BookCopies.Where(b => b.BookId == bookId).ToList();
        }

        public BookCopy? GetById(int id) => _context.BookCopies.Find(id);

        /// ✅ Đánh dấu bản sao đầu tiên có status "Available" của sách là Lost / Damaged
        public void UpdateFirstAvailableCopyStatusByBookId(int bookId, string newStatus)
        {
            var copy = _context.BookCopies
                               .FirstOrDefault(c => c.BookId == bookId && c.Status == "Available");

            if (copy != null)
            {
                copy.Status = newStatus;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Không có bản sao nào ở trạng thái 'Available' để cập nhật.");
            }
        }

        public void UpdateStatus(int copyId, string newStatus)
        {
            var copy = _context.BookCopies.Find(copyId);
            if (copy != null)
            {
                copy.Status = newStatus;
                _context.SaveChanges();
            }
        }

        // HÀM MỚI: Cập nhật trạng thái trực tiếp theo CopyId
        public void UpdateStatusByCopyId(int copyId, string newStatus)
        {
            var copy = _context.BookCopies.Find(copyId);
            if (copy != null)
            {
                copy.Status = newStatus;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Không tìm thấy bản copy với CopyId này.");
            }
        }

        public void Add(BookCopy copy)
        {
            _context.BookCopies.Add(copy);
            _context.SaveChanges();
        }

        public void Delete(int copyId)
        {
            var copy = _context.BookCopies.FirstOrDefault(c => c.CopyId == copyId);
            if (copy != null)
            {
                _context.BookCopies.Remove(copy);
                _context.SaveChanges();
            }
        }

        public BookCopy? GetAvailableCopyByBookId(int bookId)
        {
            // Trả về bản copy đầu tiên còn Available cho bookId đó
            return _context.BookCopies.FirstOrDefault(c => c.BookId == bookId && c.Status == "Available");
        }

        public void Update(BookCopy copy)
        {
            var trackedCopy = _context.BookCopies.FirstOrDefault(c => c.CopyId == copy.CopyId);
            if (trackedCopy != null)
            {
                trackedCopy.Status = copy.Status;
                // Nếu muốn update thêm trường khác, gán vào đây
                _context.SaveChanges();
            }
        }
    }
}