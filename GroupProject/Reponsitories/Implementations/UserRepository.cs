using BusinessObject;
using DataAccessLayer.DAO;
using System.Collections.Generic;

namespace Reponsitories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDao;

        public UserRepository(UserDAO userDao)
        {
            _userDao = userDao;
        }

        public List<User> GetAll() => _userDao.GetAll();
        public User? GetById(int id) => _userDao.GetById(id);
        public User? Login(string username, string password) => _userDao.Login(username, password);
        public void Add(User user) => _userDao.Add(user);
        public void Update(User user) => _userDao.Update(user);

        public void Delete(int id, int currentUserId) => _userDao.Delete(id, currentUserId);
    }
}