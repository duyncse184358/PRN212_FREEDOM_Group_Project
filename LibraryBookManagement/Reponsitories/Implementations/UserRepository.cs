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
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO dao;
        public UserRepository(LibraryDbContext context) => dao = new UserDAO(context);
        public List<User> GetAll() => dao.GetAll();
        public User GetById(int id) => dao.GetById(id);
        public User Login(string username, string passwordHash) => dao.Login(username, passwordHash);
    }
}
