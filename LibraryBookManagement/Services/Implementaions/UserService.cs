using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        public UserService(IUserRepository repository) => this.repository = repository;
        
        public List<User> GetAllUsers() => repository.GetAll();
        
        public User GetUserById(int id)
        {
            var user = repository.GetById(id);
            if (user == null) throw new Exception("User not found");
            return user;
        }
        
        public User Login(string username, string passwordHash)
        {
            return repository.Login(username, passwordHash);
        }
    }
}
