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
        public Book? SelectedBook { get; set; }
        public string SearchKeyword { get; set; } = "";

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BorrowBookCommand { get; }
        public ICommand ReturnBookCommand { get; }
        public ICommand MarkLostCommand { get; }
        public ICommand MarkDamagedCommand { get; }


        public BookViewModel(IBookService bookService, ICategoryService categoryService, IPatronService patronService, IBorrowingService borrowingService, IFineService fineService)
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
            _bookService.GetAllBooks().ForEach(b => Books.Add(b));
        }

        private void Search()
        {
            var result = _bookService.SearchBooks(SearchKeyword);
            Books.Clear();
            result.ForEach(b => Books.Add(b));
        }

        private void Add()
        {
            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.BookDialog>();
            // ĐÃ SỬA LỖI: Truyền ICategoryService khi lấy BookDialogViewModel
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<BookDialogViewModel>(
                new object[] { _categoryService });

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
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to edit.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.BookDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<BookDialogViewModel>(
                new object[] { SelectedBook!, _categoryService });

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as BookDialogViewModel;
                if (vm != null)
                {
                    vm.Book.CategoryId = vm.SelectedCategory?.CategoryId;
                    _bookService.UpdateBook(vm.Book);
                    LoadBooks();
                }
            }
        }

        private void Delete()
        {
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

            var borrowDialog = (Application.Current as App)?.Services.GetRequiredService<Views.BorrowBookDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<BorrowBookDialogViewModel>(
                new object[] { _bookService, _patronService, _borrowingService, SelectedBook! });

            if (borrowDialog.ShowDialog() == true)
            {
                LoadBooks();
                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ReturnBook()
        {
            var returnDialog = (Application.Current as App)?.Services.GetRequiredService<Views.ReturnBookDialog>();
            returnDialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<ReturnBookDialogViewModel>(
                new object[] { _bookService, _borrowingService, _patronService, _fineService });

            if (returnDialog.ShowDialog() == true)
            {
                LoadBooks();
                MessageBox.Show("Book return processed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MarkLost()
        {
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