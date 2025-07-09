using BusinessObject;
using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using LibraryWpfApp.Commands;

namespace LibraryWpfApp.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPatronService _patronService;
        private readonly IBorrowingService _borrowingService;
        private readonly IFineService _fineService;

        public ObservableCollection<Book> Books { get; set; } = new();

        private Book? _selectedBook;
        public Book? SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
        }

        private string _searchKeyword = "";
        public string SearchKeyword
        {
            get => _searchKeyword;
            set => SetProperty(ref _searchKeyword, value);
        }

        public bool CanManageBooks => AppContext.IsAdmin || AppContext.IsLibrarian || AppContext.IsStaff;

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BorrowBookCommand { get; }
        public ICommand ReturnBookCommand { get; }
        public ICommand MarkLostCommand { get; }
        public ICommand MarkDamagedCommand { get; }

        // Parameterless constructor for XAML (not used with DI)
        public BookViewModel() : this(
            (Application.Current as App)?.Services?.GetService<IBookService>()!,
            (Application.Current as App)?.Services?.GetService<ICategoryService>()!,
            (Application.Current as App)?.Services?.GetService<IPatronService>()!,
            (Application.Current as App)?.Services?.GetService<IBorrowingService>()!,
            (Application.Current as App)?.Services?.GetService<IFineService>()!
        )
        {
            if (_bookService == null) Console.WriteLine("BookService is null in BookViewModel parameterless constructor!");
            if (_categoryService == null) Console.WriteLine("CategoryService is null in BookViewModel parameterless constructor!");
        }

        public BookViewModel(
            IBookService bookService,
            ICategoryService categoryService,
            IPatronService patronService,
            IBorrowingService borrowingService,
            IFineService fineService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _patronService = patronService;
            _borrowingService = borrowingService;
            _fineService = fineService;

            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            BorrowBookCommand = new RelayCommand(BorrowBook);
            ReturnBookCommand = new RelayCommand(ReturnBook);
            MarkLostCommand = new RelayCommand(MarkLost);
            MarkDamagedCommand = new RelayCommand(MarkDamaged);

            LoadBooks();
        }

        private void LoadBooks()
        {
            Books.Clear();
            if (_bookService != null)
            {
                _bookService.GetAllBooks().ForEach(b => Books.Add(b));
               
            }
            else
            {
                Console.WriteLine("Error: _bookService is null when trying to load books.");
                MessageBox.Show("Cannot load books: Service not initialized. Please restart application.", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search()
        {
            if (_bookService == null)
            {
                MessageBox.Show("Service not available. Cannot search books.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = _bookService.SearchBooks(SearchKeyword);
            Books.Clear();
            result.ForEach(b => Books.Add(b));
        }

        private void Add()
        {
            if (!CanManageBooks)
            {
                MessageBox.Show("You do not have permission to add books.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if ((Application.Current as App)?.Services == null) return;

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.BookDialog>();
            dialog!.DataContext = ActivatorUtilities.CreateInstance<ViewModels.BookDialogViewModel>(
                (Application.Current as App)?.Services!,
                _categoryService
            );

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as BookDialogViewModel;
                if (vm != null)
                {
                    vm.Book.CategoryId = vm.SelectedCategory?.CategoryId;
                    _bookService.AddBook(vm.Book);
                    LoadBooks();
                }
            }
        }

        private void Edit()
        {
            if (!CanManageBooks)
            {
                MessageBox.Show("You do not have permission to edit books.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to edit.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if ((Application.Current as App)?.Services == null) return;

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.BookDialog>();
            dialog!.DataContext = ActivatorUtilities.CreateInstance<ViewModels.BookDialogViewModel>(
                (Application.Current as App)?.Services!,
                SelectedBook!,
                _categoryService
            );

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as BookDialogViewModel;
                if (vm != null)
                {
                    _bookService.UpdateBook(vm.Book);
                    LoadBooks();
                }
            }
        }

        private void Delete()
        {
            if (!CanManageBooks)
            {
                MessageBox.Show("You do not have permission to delete books.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to delete.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var activeBorrowings = _borrowingService.GetAllBorrowings().Any(b => b.BookId == SelectedBook.BookId && (b.IsReturned == null || b.IsReturned == false));
            if (activeBorrowings)
            {
                MessageBox.Show("Cannot delete a book that is currently borrowed.", "Deletion Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete book '{SelectedBook.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _bookService.DeleteBook(SelectedBook.BookId);
                LoadBooks();
            }
        }

        private void BorrowBook()
        {
            if (!AppContext.IsMember && !AppContext.IsLibrarian && !AppContext.IsStaff)
            {
                MessageBox.Show("You do not have permission to borrow books.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to borrow.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBook.AvailableCopies <= 0 || SelectedBook.Status != "Available")
            {
                MessageBox.Show("This book is not available for borrowing.", "Not Available", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if ((Application.Current as App)?.Services == null) return;

            var borrowDialog = (Application.Current as App)?.Services.GetRequiredService<Views.BorrowBookDialog>();
            borrowDialog!.DataContext = ActivatorUtilities.CreateInstance<ViewModels.BorrowBookDialogViewModel>(
                (Application.Current as App)?.Services!,
                _bookService,
                _patronService,
                _borrowingService,
                SelectedBook!
            );

            if (borrowDialog.ShowDialog() == true)
            {
                LoadBooks();
                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ReturnBook()
        {
            // Cho phép trả sách nếu người dùng là thành viên hoặc có quyền quản lý (admin, librarian, staff)
            if (!(AppContext.IsMember || CanManageBooks))
            {
                MessageBox.Show("Bạn không có quyền trả sách.", "Không đủ quyền hạn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if ((Application.Current as App)?.Services == null)
                return;

            // Tạo dialog trả sách sử dụng DI
            var returnDialog = (Application.Current as App)?.Services.GetRequiredService<Views.ReturnBookDialog>();
            returnDialog!.DataContext = ActivatorUtilities.CreateInstance<ViewModels.ReturnBookDialogViewModel>(
                (Application.Current as App)?.Services!,
                _bookService,
                _borrowingService,
                _patronService,
                _fineService
            );

            // Hiển thị dialog. Nếu dialog trả về true (tức là xử lý trả sách thành công)
            if (returnDialog.ShowDialog() == true)
            {
                // Làm mới danh sách sách
                LoadBooks();
                MessageBox.Show("Quá trình trả sách đã được xử lý thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MarkLost()
        {
            if (!CanManageBooks)
            {
                MessageBox.Show("You do not have permission to mark books as lost.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to mark as lost.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Are you sure you want to mark '{SelectedBook.Title}' as LOST? This will reduce available copies.", "Confirm Lost", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _bookService.MarkBookStatus(SelectedBook.BookId, "Lost");
                LoadBooks();
                MessageBox.Show("Book marked as lost.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MarkDamaged()
        {
            if (!CanManageBooks)
            {
                MessageBox.Show("You do not have permission to mark books as damaged.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to mark as damaged.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Are you sure you want to mark '{SelectedBook.Title}' as DAMAGED? This will reduce available copies.", "Confirm Damaged", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _bookService.MarkBookStatus(SelectedBook.BookId, "Damaged");
                LoadBooks();
                MessageBox.Show("Book marked as damaged.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
