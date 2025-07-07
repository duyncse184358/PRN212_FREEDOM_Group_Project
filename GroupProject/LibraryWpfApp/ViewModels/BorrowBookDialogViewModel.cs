using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using LibraryWpfApp.Commands; // ĐÃ SỬA NAMESPACE THÀNH LibraryWpfApp.Commands
using Services;
using System.Windows.Input;
using System.Windows; // Cần thêm using System.Windows
using Microsoft.Extensions.DependencyInjection; // Cần cho GetRequiredService

namespace LibraryWpfApp.ViewModels // ĐÃ SỬA NAMESPACE THÀNH LibraryWpfApp.ViewModels
{
    public class BorrowBookDialogViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IBorrowingService _borrowingService;

        public Book BookToBorrow { get; set; }
        public ObservableCollection<Patron> Patrons { get; set; } = new();
        public Patron? SelectedPatron { get; set; }

        public ICommand ConfirmBorrowCommand { get; }

        // Constructor mặc định (public parameterless constructor) cho XAML
        public BorrowBookDialogViewModel()
            : this(
                (Application.Current as App)?.Services.GetRequiredService<IBookService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IPatronService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IBorrowingService>()!,
                new BusinessObject.Book() // Cung cấp một Book mặc định nếu không có
            )
        {
            // Constructor này được gọi bởi XAML.
            // Nó ủy quyền việc khởi tạo thực tế cho constructor có tham số,
            // lấy các service và một Book mặc định từ DI container.
        }

        public BorrowBookDialogViewModel(IBookService bookService, IPatronService patronService, IBorrowingService borrowingService, Book book)
        {
            _bookService = bookService;
            _patronService = patronService;
            _borrowingService = borrowingService;
            BookToBorrow = book;

            LoadPatrons();
            ConfirmBorrowCommand = new RelayCommand(ConfirmBorrow);
        }

        private void LoadPatrons()
        {
            Patrons = new ObservableCollection<Patron>(_patronService.GetAllPatrons());
            SelectedPatron = Patrons.FirstOrDefault();
        }

        private void ConfirmBorrow()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newBorrowing = new BusinessObject.Borrowing
                {
                    BookId = BookToBorrow.BookId,
                    PatronId = SelectedPatron.PatronId,
                    BorrowDate = DateOnly.FromDateTime(DateTime.Now),
                    DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
                    IsReturned = false,
                    Status = "Borrowed"
                };
                _borrowingService.BorrowBook(newBorrowing);

                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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