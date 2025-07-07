using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Services;
using LibraryWpfApp.ViewModels;
using System.Windows; // Đảm bảo namespace này khớp với project của bạn

namespace LibraryWpfApp.ViewModels // ĐÃ SỬA NAMESPACE THÀNH LibraryWpfApp.ViewModels
{
    public class BookDialogViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;
        public Book Book { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public Category? SelectedCategory { get; set; }

        public ObservableCollection<string> BookStatuses { get; set; } = new ObservableCollection<string>
        {
            "Available", "Borrowed", "Lost", "Damaged", "Missing"
        };

        // Constructor mặc định (public parameterless constructor) cho XAML
        public BookDialogViewModel()
            
        {
        }

        public BookDialogViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Book = new BusinessObject.Book();
            LoadCategories();
            SelectedCategory = Categories.FirstOrDefault();
            Book.Status = BookStatuses.FirstOrDefault() ?? "Available";
        }

        public BookDialogViewModel(Book originalBook, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Book = new BusinessObject.Book
            {
                BookId = originalBook.BookId,
                Isbn = originalBook.Isbn,
                Title = originalBook.Title,
                Author = originalBook.Author,
                Publisher = originalBook.Publisher,
                PublicationYear = originalBook.PublicationYear,
                Genre = originalBook.Genre,
                NumberOfCopies = originalBook.NumberOfCopies,
                AvailableCopies = originalBook.AvailableCopies,
                ShelfLocation = originalBook.ShelfLocation,
                Status = originalBook.Status,
                CategoryId = originalBook.CategoryId
            };
            LoadCategories();
            SelectedCategory = Categories.FirstOrDefault(c => c.CategoryId == Book.CategoryId);
        }

        private void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(_categoryService.GetAllCategories());
        }
    }
}