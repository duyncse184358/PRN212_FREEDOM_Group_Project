using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BusinessObject;
using LibraryWpfApp.Commands;
using Services;

namespace LibraryWpfApp.ViewModels
{
    public class BorrowBookDialogViewModel : BaseViewModel
    {
        private readonly IBookService? _bookService;
        private readonly IPatronService? _patronService;
        private readonly IBorrowingService? _borrowingService;

        public Book BookToBorrow { get; set; } = new Book();
        private ObservableCollection<Patron> _patrons = new();
        public ObservableCollection<Patron> Patrons
        {
            get => _patrons;
            set { _patrons = value; OnPropertyChanged(); }
        }
        private Patron? _selectedPatron;
        public Patron? SelectedPatron
        {
            get => _selectedPatron;
            set { _selectedPatron = value; OnPropertyChanged(); }
        }

        public ICommand ConfirmBorrowCommand { get; }
        public Borrowing? LastBorrowedRecord { get; private set; }

        // Constructor mặc định cho XAML (không dùng thực tế)
        public BorrowBookDialogViewModel()
        {
            ConfirmBorrowCommand = new RelayCommand(() =>
            {
                MessageBox.Show("Vui lòng không mở dialog này trực tiếp từ XAML!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        // Constructor thực tế dùng DI
        public BorrowBookDialogViewModel(IBookService bookService, IPatronService patronService, IBorrowingService borrowingService, Book book)
        {
            _bookService = bookService;
            _patronService = patronService;
            _borrowingService = borrowingService;
            BookToBorrow = book ?? new Book();

            LoadPatrons();
            ConfirmBorrowCommand = new RelayCommand(ConfirmBorrow);
        }

        private void LoadPatrons()
        {
            if (_patronService != null)
            {
                Patrons = new ObservableCollection<Patron>(_patronService.GetAllPatrons());
                SelectedPatron = Patrons.FirstOrDefault();
            }
        }

        private void ConfirmBorrow()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_borrowingService == null)
            {
                MessageBox.Show("Borrowing service is not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra số lượng sách khả dụng
            if (BookToBorrow.AvailableCopies <= 0)
            {
                MessageBox.Show("No available copies for this book.", "Unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                //var borrowDate = DateOnly.FromDateTime(DateTime.Now);
                //var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

                var borrowDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)); // Giả lập mượn sách từ 10 ngày trước
                var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3));    // Hạn trả đã qua 3 ngày

                _borrowingService.BorrowBook(
                    BookToBorrow.BookId,
                    SelectedPatron.PatronId,
                    borrowDate,
                    dueDate
                );

                LastBorrowedRecord = new Borrowing
                {
                    BookId = BookToBorrow.BookId,
                    PatronId = SelectedPatron.PatronId,
                    BorrowDate = borrowDate,
                    DueDate = dueDate,
                    IsReturned = false,
                    Status = "Borrowed"
                };

                System.Diagnostics.Debug.WriteLine($"[BorrowBookDialogViewModel] Borrowed BookId={BookToBorrow.BookId} for PatronId={SelectedPatron.PatronId}");

                //MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Đóng dialog (thiết lập DialogResult)
                var dialog = Application.Current.Windows.OfType<Views.BorrowBookDialog>().FirstOrDefault(w => w.DataContext == this);
                if (dialog != null)
                {
                    dialog.DialogResult = true;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Borrow Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
