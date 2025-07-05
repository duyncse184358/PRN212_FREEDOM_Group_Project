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
    public class FineRepository : IFineRepository
    {
        private readonly FineDAO dao;
        public FineRepository(LibraryDbContext context) => dao = new FineDAO(context);
        public List<Fine> GetByPatronId(int patronId) => dao.GetByPatronId(patronId);
        public void Add(Fine f) => dao.Add(f);
        public void MarkAsPaid(int fineId) => dao.MarkAsPaid(fineId);
    }
}
