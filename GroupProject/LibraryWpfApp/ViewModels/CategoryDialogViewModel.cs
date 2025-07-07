using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace LibraryWpfApp.ViewModels
{
    public class CategoryDialogViewModel : BaseViewModel
    {
        public Category Category { get; set; }

        public CategoryDialogViewModel()
        {
            Category = new Category();
        }
        
        public CategoryDialogViewModel(Category original)
        {
            Category = new BusinessObject.Category
            {
                CategoryId = original.CategoryId,
                CategoryName = original.CategoryName,
                // Description field is not in your new Category table.
                // If you add it to the DB, uncomment this: Description = original.Description
            };
        }
    }
}
