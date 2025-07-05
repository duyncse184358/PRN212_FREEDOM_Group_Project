using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User Login(string username, string passwordHash);
    }
}
