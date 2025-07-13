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

        // THÊM CÁC PROPERTIES MỚI ĐÂY
        private string _patronId = "";
        public string PatronId
        {
            get => _patronId;
            set
            {
                _patronId = value;
                OnPropertyChanged();
                LoadPatronName(); // Tự động load tên khi ID thay đổi
            }
        }

        private string _patronName = "";
        public string PatronName
        {
            get => _patronName;
            set
            {
                _patronName = value;
                OnPropertyChanged();
            }
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

        // THÊM METHOD MỚI ĐÂY
        private void LoadPatronName()
        {
            if (!string.IsNullOrEmpty(PatronId) && _patronService != null)
            {
                try
                {
                    // Tìm patron theo ID (giả sử PatronId là string, nếu là int thì parse trước)
                    var patron = _patronService.GetAllPatrons().FirstOrDefault(p => p.PatronId.ToString() == PatronId);

                    if (patron != null)
                    {
                        PatronName = patron.FullName;
                        SelectedPatron = patron; // Cập nhật SelectedPatron để logic cũ vẫn hoạt động
                    }
                    else
                    {
                        PatronName = "Patron not found";
                        SelectedPatron = null;
                    }
                }
                catch (Exception)
                {
                    PatronName = "Error loading patron";
                    SelectedPatron = null;
                }
            }
            else
            {
                PatronName = "";
                SelectedPatron = null;
            }
        }

        private void ConfirmBorrow()
        {
            // Kiểm tra PatronId thay vì SelectedPatron
            if (string.IsNullOrEmpty(PatronId) || SelectedPatron == null)
            {
                MessageBox.Show("Please enter a valid Patron ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                // Thiết lập ngày mượn và hạn trả
                var borrowDate = DateOnly.FromDateTime(DateTime.Now); // Ngày mượn là ngày hiện tại
                var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14)); // Hạn trả là 14 ngày sau

                // Gọi phương thức BorrowBook và lấy bản ghi mượn sách được trả về
                LastBorrowedRecord = _borrowingService.BorrowBook(
                    BookToBorrow.BookId, // ID của sách
                    SelectedPatron.PatronId, // ID của bạn đọc
                    borrowDate, // Ngày mượn
                    dueDate // Hạn trả
                );

                // Kiểm tra xem bản ghi mượn có được trả về chính xác không
                if (LastBorrowedRecord == null || LastBorrowedRecord.BorrowingId == 0)
                {
                    throw new Exception("Failed to create borrowing record. BorrowingId is missing.");
                }

                // Ghi log thông tin mượn sách (chỉ để kiểm tra)
                System.Diagnostics.Debug.WriteLine($"[BorrowBookDialogViewModel] Borrowed BookId={LastBorrowedRecord.BookId} for PatronId={LastBorrowedRecord.PatronId}, BorrowingId={LastBorrowedRecord.BorrowingId}");

                // Đóng dialog (thiết lập DialogResult)
                var dialog = Application.Current.Windows.OfType<Views.BorrowBookDialog>().FirstOrDefault(w => w.DataContext == this);
                if (dialog != null)
                {
                    dialog.DialogResult = true; // Đánh dấu dialog thành công
                }
            }
            catch (InvalidOperationException ex)
            {
                // Xử lý lỗi logic trong quá trình mượn sách
                MessageBox.Show(ex.Message, "Borrow Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Xử lý mọi lỗi không mong muốn
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
