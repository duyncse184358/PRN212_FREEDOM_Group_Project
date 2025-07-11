using BusinessObject;
using DataAccessLayer.DAO;
using System.Collections.Generic;

namespace Reponsitories.Implementations
{
    public class FineRepository : IFineRepository
    {
        private readonly FineDAO _fineDao;

        public FineRepository(FineDAO fineDao)
        {
            _fineDao = fineDao;
        }

        public List<Fine> GetAll() => _fineDao.GetAll();
        public Fine? GetById(int id) => _fineDao.GetById(id);
        public List<Fine> GetByPatronId(int patronId) => _fineDao.GetByPatronId(patronId);
        public void Add(Fine f) => _fineDao.Add(f);
        public void Update(Fine fine) => _fineDao.Update(fine);
        public void MarkAsPaid(int fineId) => _fineDao.MarkAsPaid(fineId);
        public void Delete(int fineId) => _fineDao.Delete(fineId);
    }
}