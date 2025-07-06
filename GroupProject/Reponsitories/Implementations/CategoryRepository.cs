using BusinessObject;
using DataAccessLayer.DAO;
using System.Collections.Generic;

namespace Reponsitories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDao;

        public CategoryRepository(CategoryDAO categoryDao)
        {
            _categoryDao = categoryDao;
        }

        public List<Category> GetAll() => _categoryDao.GetAll();
        public Category? GetById(int id) => _categoryDao.GetById(id);
        public void Add(Category category) => _categoryDao.Add(category);
        public void Update(Category category) => _categoryDao.Update(category);
        public void Delete(int id) => _categoryDao.Delete(id);
    }
}