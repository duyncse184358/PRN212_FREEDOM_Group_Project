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
        public User? GetById(int id) => _context.Users.Find(id);

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User? Login(string username, string password) =>
            _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
    }
}