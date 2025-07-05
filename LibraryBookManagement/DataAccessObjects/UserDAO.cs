using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class UserDAO
    {
        private readonly LibraryDbContext _context;

        public UserDAO(LibraryDbContext context) => _context = context;

        public List<User> GetAll() => _context.Users.ToList();
        public User GetById(int id) => _context.Users.Find(id);

        public User Login(string username, string passwordHash) =>
            _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
    }
}
