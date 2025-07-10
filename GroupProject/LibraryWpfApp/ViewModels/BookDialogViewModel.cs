using System.Collections.ObjectModel;
using System.Linq;
using BusinessObject;
using Services;

namespace LibraryWpfApp.ViewModels
{
    public class BookDialogViewModel : BaseViewModel
    {
        private readonly ICategoryService? _categoryService;

        private Book _book = new Book();
        public Book Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }

        private ObservableCollection<Category> _categories = new();
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value) && value != null)
                {
                    Book.CategoryId = value.CategoryId;
                }
            }
        }

        public ObservableCollection<string> BookStatuses { get; set; } = new()
        {
            "Available", "Borrowed", "Lost", "Damaged", "Missing"
        };

        public BookDialogViewModel()
        {
            Book = new Book();
            Categories = new ObservableCollection<Category>();
        }

        public BookDialogViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Book = new Book();
            LoadCategories();
            SelectedCategory = Categories.FirstOrDefault();
            Book.Status = BookStatuses.FirstOrDefault() ?? "Available";
        }

        public BookDialogViewModel(Book originalBook, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Book = new Book
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
                CategoryId = originalBook.CategoryId,
                Price = originalBook.Price // thêm dòng này
            };
            LoadCategories();
            SelectedCategory = Categories.FirstOrDefault(c => c.CategoryId == Book.CategoryId);
        }

        private void LoadCategories()
        {
            if (_categoryService != null)
                Categories = new ObservableCollection<Category>(_categoryService.GetAllCategories());
        }
    }
}
