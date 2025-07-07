using BusinessObject;
using DataAccessLayer.DAO;
using System.Collections.Generic;

namespace Reponsitories.Implementations
{
    public class PatronRepository : IPatronRepository
    {
        private readonly PatronDAO _patronDao;

        public PatronRepository(PatronDAO patronDao)
        {
            _patronDao = patronDao;
        }

        public List<Patron> GetAll() => _patronDao.GetAll();
        public Patron? GetById(int id) => _patronDao.GetById(id);
        public void Add(Patron patron) => _patronDao.Add(patron);
        public void Update(Patron patron) => _patronDao.Update(patron);
        public void Delete(int id) => _patronDao.Delete(id);

        public List<Patron> Search(string keyword)
        {
            throw new NotImplementedException();
        }
        //   public List<Patron> Search(string keyword) => _patronDao.Search(keyword);
    }
}