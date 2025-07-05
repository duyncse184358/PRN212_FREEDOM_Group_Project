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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO dao;
        public CategoryRepository(LibraryDbContext context) => dao = new CategoryDAO(context);
        public List<Category> GetAll() => dao.GetAll();
        public Category GetById(int id) => dao.GetById(id);
        public void Add(Category category) => dao.Add(category);
        public void Update(Category category) => dao.Update(category);
        public void Delete(int id) => dao.Delete(id);
    }
}
