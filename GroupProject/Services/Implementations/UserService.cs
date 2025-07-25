using BusinessObject;
using Reponsitories;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public List<User> GetAllUsers() => _repo.GetAll();
        public User? GetUserById(int id) => _repo.GetById(id);
        public User? LoginUser(string username, string password)
        {
           
            return _repo.Login(username, password);
        }
        public void AddUser(User user)
        {
            var existingUser = _repo.GetAll().FirstOrDefault(u => u.UserName == user.UserName);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            _repo.Add(user);
        }
        public void UpdateUser(User user) => _repo.Update(user);
      

        public void DeleteUser(int id, int currentUserId) => _repo.Delete(id, currentUserId);
    }
}