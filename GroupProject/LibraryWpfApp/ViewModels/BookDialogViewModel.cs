using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using BusinessObject;
using LibraryWpfApp.Commands;
using Microsoft.Win32;
using Services;
using System.IO;
namespace LibraryWpfApp.ViewModels
{
    public class BookDialogViewModel : BaseViewModel
    {
        private readonly ICategoryService? _categoryService;
        public RelayCommand ChooseImageCommand { get; set; }
        private Book _book = new Book();
        public Book Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }


        public BitmapImage? BookImageSource
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(Book.ImagePath) || !File.Exists(Book.ImagePath))
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/Images/default.png"));
                    }

                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(Book.ImagePath, UriKind.Absolute);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    return image;
                }
                catch
                {
                    // Trả lại ảnh mặc định nếu có lỗi
                    return new BitmapImage(); // Không có UriSource, là ảnh rỗng

                }
            }
        }


        private void UpdateImagePreview() =>
            OnPropertyChanged(nameof(BookImageSource));

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
            ChooseImageCommand = new RelayCommand(ChooseImage);

        }

        public BookDialogViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Book = new Book();
            LoadCategories();
            SelectedCategory = Categories.FirstOrDefault();
            Book.Status = BookStatuses.FirstOrDefault() ?? "Available";
            ChooseImageCommand = new RelayCommand(ChooseImage);

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
                Price = originalBook.Price ,// thêm dòng này
                ImagePath = originalBook.ImagePath
            };
            LoadCategories();
            SelectedCategory = Categories.FirstOrDefault(c => c.CategoryId == Book.CategoryId);
            ChooseImageCommand = new RelayCommand(ChooseImage);

        }
        private void ChooseImage(object? _)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Select Book Image"

            };

            if (openFileDialog.ShowDialog() == true)
            {
                Book.ImagePath = openFileDialog.FileName;
                OnPropertyChanged(nameof(Book));               // báo đổi toàn bộ Book
                OnPropertyChanged(nameof(BookImageSource));     // báo đổi riêng ảnh
            }
        }


        private void LoadCategories()
        {
            if (_categoryService != null)
                Categories = new ObservableCollection<Category>(_categoryService.GetAllCategories());
        }
    }
}
