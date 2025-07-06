using BusinessObject;
using DataAccessLayer.DAO;
using System.Collections.Generic;

namespace Reponsitories.Implementations
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly BorrowingDAO _borrowingDao;

        public BorrowingRepository(BorrowingDAO borrowingDao)
        {
            _borrowingDao = borrowingDao;
        }

        public List<Borrowing> GetAll() => _borrowingDao.GetAll();
        public Borrowing? GetById(int id) => _borrowingDao.GetById(id);
        public List<Borrowing> GetByPatronId(int patronId) => _borrowingDao.GetByPatronId(patronId);
        public void Add(Borrowing borrowing) => _borrowingDao.Add(borrowing);
        public void Update(Borrowing borrowing) => _borrowingDao.Update(borrowing);
        public void MarkReturned(int borrowingId) => _borrowingDao.MarkReturned(borrowingId);
    }
}