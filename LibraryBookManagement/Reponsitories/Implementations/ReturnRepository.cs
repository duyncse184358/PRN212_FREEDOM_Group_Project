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
    public class ReturnRepository : IReturnRepository
    {
        private readonly ReturnDAO dao;
        public ReturnRepository(LibraryDbContext context) => dao = new ReturnDAO(context);
        public List<Return> GetAll() => dao.GetAll();
        public Return GetByBorrowingId(int borrowingId) => dao.GetByBorrowingId(borrowingId);
        public void Add(Return r) => dao.Add(r);
    }
}
