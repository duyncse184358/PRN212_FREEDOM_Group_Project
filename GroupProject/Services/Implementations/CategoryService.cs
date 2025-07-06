using BusinessObject;
using Reponsitories;
using Services;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public List<Category> GetAllCategories() => _repo.GetAll();
        public Category? GetCategoryById(int id) => _repo.GetById(id);
        public void AddCategory(Category category) => _repo.Add(category);
        public void UpdateCategory(Category category) => _repo.Update(category);
        public void DeleteCategory(int id) => _repo.Delete(id);
    }
}