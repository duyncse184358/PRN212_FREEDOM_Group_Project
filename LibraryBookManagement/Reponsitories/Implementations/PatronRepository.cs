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
    public class PatronRepository : IPatronRepository
    {
        private readonly PatronDAO dao;
        public PatronRepository(LibraryDbContext context) => dao = new PatronDAO(context);
        public List<Patron> GetAll() => dao.GetAll();
        public Patron GetById(int id) => dao.GetById(id);
        public void Add(Patron patron) => dao.Add(patron);
        public void Update(Patron patron) => dao.Update(patron);
        public void Delete(int id) => dao.Delete(id);
    }

}
