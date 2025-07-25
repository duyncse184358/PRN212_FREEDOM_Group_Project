using BusinessObject;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            // Detach any existing tracked entity with the same key
            var existingEntity = _context.Entry(user);
            if (existingEntity.State == EntityState.Detached)
            {
                var trackedEntity = _context.Users.Local.FirstOrDefault(u => u.UserId == user.UserId);
                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Detached;
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        //public void Delete(int id)
        //{
        //    var user = _context.Users.Find(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //        _context.SaveChanges();
        //    }
        //}

        public void Delete(int userIdToDelete, int currentUserId)
        {
            var userToDelete = _context.Users.Find(userIdToDelete);
            if (userToDelete == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Không được xóa tài khoản đang đăng nhập
            if (userIdToDelete == currentUserId)
            {
                throw new InvalidOperationException("You cannot delete the account you are currently logged in with.");
            }

            // Đếm số lượng Admin
            int adminCount = _context.Users.Count(u => u.Role == "Admin");

            // Không cho phép xóa Admin cuối cùng
            if (userToDelete.Role == "Admin" && adminCount <= 1)
            {
                throw new InvalidOperationException("At least one Admin account must remain in the system.");
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }



        public User? Login(string username, string password) =>
            _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
    }
}