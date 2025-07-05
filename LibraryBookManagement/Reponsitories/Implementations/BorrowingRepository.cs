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
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly BorrowingDAO dao;
        public BorrowingRepository(LibraryDbContext context) => dao = new BorrowingDAO(context);
        public List<Borrowing> GetAll() => dao.GetAll();
        public List<Borrowing> GetByPatronId(int patronId) => dao.GetByPatronId(patronId);
        public void Add(Borrowing borrowing) => dao.Add(borrowing);
        public void MarkReturned(int borrowingId) => dao.MarkReturned(borrowingId);
    }
}
