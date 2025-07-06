using BusinessObject;
using System.Collections.Generic;

namespace Reponsitories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}