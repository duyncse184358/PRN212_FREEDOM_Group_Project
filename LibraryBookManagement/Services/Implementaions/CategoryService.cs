using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class CategoryService
    {
        private readonly ICategoryRepository repository;
        public CategoryService(ICategoryRepository repository) => this.repository = repository;

        public List<Category> GetAllCategories() => repository.GetAll();

        public void AddCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name)) throw new ArgumentException("Category name is required");
            repository.Add(category);
        }

        public void UpdateCategory(Category category) => repository.Update(category);
        public void DeleteCategory(int id) => repository.Delete(id);
    }

}
