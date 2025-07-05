using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Services
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        Role GetRoleById(int id);
    }
}
