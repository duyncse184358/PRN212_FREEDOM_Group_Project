using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;
        public RoleService(IRoleRepository repository) => this.repository = repository;

        public List<Role> GetAllRoles() => repository.GetAll();
        public Role GetRoleById(int id) => repository.GetById(id);
    }
}
