using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class CategoryDAO
    {
        private readonly LibraryDbContext _context;

        public CategoryDAO(LibraryDbContext context) => _context = context;

        public List<Category> GetAll() => _context.Categories.ToList();
        public Category? GetById(int id) => _context.Categories.Find(id);

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            // Tìm entity đã được track trong context
            var existingCategory = _context.Categories.Find(category.CategoryId);
            if (existingCategory != null)
            {
                // Cập nhật thuộc tính của entity đã được track
                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
            }
        }
    }
}