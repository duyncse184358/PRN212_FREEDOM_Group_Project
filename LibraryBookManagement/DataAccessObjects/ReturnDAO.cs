using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class ReturnDAO
    {
        private readonly LibraryDbContext _context;

        public ReturnDAO(LibraryDbContext context) => _context = context;

        public List<Return> GetAll() => _context.Returns.ToList();
        public Return GetByBorrowingId(int borrowingId) =>
            _context.Returns.FirstOrDefault(r => r.BorrowingId == borrowingId);

        public void Add(Return r)
        {
            _context.Returns.Add(r);
            _context.SaveChanges();
        }
    }
}
