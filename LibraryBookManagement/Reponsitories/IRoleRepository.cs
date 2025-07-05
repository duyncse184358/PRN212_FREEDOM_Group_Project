using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IRoleRepository
    {
        List<Role> GetAll();
        Role GetById(int id);
    }
}
