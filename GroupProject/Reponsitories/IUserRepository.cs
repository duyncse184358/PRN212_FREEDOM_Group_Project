using BusinessObject;
using System.Collections.Generic;

namespace Reponsitories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int id);
        User? Login(string username, string password);
        void Add(User user);
        void Update(User user);
        void Delete(int id, int currentUserId);
    }
}