using BusinessObject;
using System.Collections.Generic;

namespace Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User? GetUserById(int id);
        User? LoginUser(string username, string password);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id, int currentUserId);
    }
}