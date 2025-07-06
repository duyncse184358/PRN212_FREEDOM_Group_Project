using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class PatronDAO
    {
        private readonly LibraryDbContext _context;

        public PatronDAO(LibraryDbContext context) => _context = context;

        public List<Patron> GetAll() => _context.Patrons.ToList();
        public Patron? GetById(int id) => _context.Patrons.Find(id);

        public void Add(Patron patron)
        {
            _context.Patrons.Add(patron);
            _context.SaveChanges();
        }

        public void Update(Patron patron)
        {
            _context.Patrons.Update(patron);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = _context.Patrons.Find(id);
            if (p != null)
            {
                _context.Patrons.Remove(p);
                _context.SaveChanges();
            }
        }
    }
}