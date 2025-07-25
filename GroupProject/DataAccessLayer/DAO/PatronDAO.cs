using BusinessObject;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            // Detach any existing tracked entity with the same key
            var existingEntity = _context.Entry(patron);
            if (existingEntity.State == EntityState.Detached)
            {
                var trackedEntity = _context.Patrons.Local.FirstOrDefault(p => p.PatronId == patron.PatronId);
                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Detached;
                }
            }

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

        public List<Patron> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _context.Patrons
                .Where(p => p.FullName.Contains(keyword) ||
                           p.Email.Contains(keyword) ||
                           p.Phone.Contains(keyword) ||
                           p.Address.Contains(keyword))
                .ToList();
        }
    }
}