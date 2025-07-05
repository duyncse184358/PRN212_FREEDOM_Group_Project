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
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleDAO dao;
        public RoleRepository(LibraryDbContext context) => dao = new RoleDAO(context);
        public List<Role> GetAll() => dao.GetAll();
        public Role GetById(int id) => dao.GetById(id);
    }
}
