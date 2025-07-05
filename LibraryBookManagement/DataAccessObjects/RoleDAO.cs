using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class RoleDAO
    {
        private readonly LibraryDbContext _context;

        public RoleDAO(LibraryDbContext context) => _context = context;

        public List<Role> GetAll() => _context.Roles.ToList();
        public Role GetById(int id) => _context.Roles.Find(id);
    }
}
