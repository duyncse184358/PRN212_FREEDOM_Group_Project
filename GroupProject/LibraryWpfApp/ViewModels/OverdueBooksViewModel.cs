using System.Collections.ObjectModel;
using Services;
using System.Windows.Input;
using System.Windows; // Cần thêm using System.Windows
using Microsoft.Extensions.DependencyInjection;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;

namespace LibraryWpfApp.ViewModels // Đã cập nhật namespace
{
    public class OverdueBooksViewModel : BaseViewModel
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IFineService _fineService;

        public ObservableCollection<BorrowingDisplayModel> OverdueBorrowings { get; set; } = new();
        public BorrowingDisplayModel? SelectedOverdueBorrowing { get; set; }

        public ICommand ReloadOverdueCommand { get; }
        public ICommand ProcessFineCommand { get; }

        public OverdueBooksViewModel(IBorrowingService borrowingService, IBookService bookService, IPatronService patronService, IFineService fineService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
            _patronService = patronService;
            _fineService = fineService;

            ReloadOverdueCommand = new RelayCommand(LoadOverdueBooks);
            ProcessFineCommand = new RelayCommand(ProcessFine);

            LoadOverdueBooks();
        }

        private void LoadOverdueBooks()
        {
            OverdueBorrowings.Clear();
            var overdueList = _borrowingService.GetOverdueBorrowings();

            foreach (var b in overdueList)
            {
                var book = _bookService.GetBookById(b.BookId ?? 0);
                var patron = _patronService.GetPatronById(b.PatronId ?? 0);
                var fine = _fineService.GetAllFines().FirstOrDefault(f => f.BorrowingId == b.BorrowingId && (f.Paid == null || f.Paid == false));

                // ĐÃ SỬA LỖI: Thay Borrowings.Add bằng OverdueBorrowings.Add
                OverdueBorrowings.Add(new BorrowingDisplayModel
                {
                    BorrowingID = b.BorrowingId,
                    BookTitle = book?.Title ?? "Unknown Book",
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    PatronID = b.PatronId ?? 0,
                    BorrowDate = b.BorrowDate.ToDateTime(TimeOnly.MinValue),
                    DueDate = b.DueDate.ToDateTime(TimeOnly.MinValue),
                    ReturnDate = b.ReturnDate?.ToDateTime(TimeOnly.MinValue),
                    Status = b.Status ?? "Unknown",
                    FineAmount = fine?.FineAmount ?? 0M,
                    IsFinePaid = fine?.Paid ?? true
                });
            }
        }

        private void ProcessFine()
        {
            if (SelectedOverdueBorrowing == null)
            {
                MessageBox.Show("Please select an overdue record to process fine.", "No Record Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var originalBorrowing = _borrowingService.GetBorrowingById(SelectedOverdueBorrowing.BorrowingID);
            if (originalBorrowing == null) return;

            decimal fineAmount = _borrowingService.CalculateFine(originalBorrowing);

            if (fineAmount > 0)
            {
                var fineVm = (Application.Current as App)?.Services.GetRequiredService<ViewModels.FinePaymentDialogViewModel>();
                fineVm!.Setup(originalBorrowing.BorrowingId,
                               originalBorrowing.PatronId ?? 0,
                               fineAmount,
                               _patronService.GetPatronById(originalBorrowing.PatronId ?? 0)?.FullName ?? "Unknown Patron");

                var fineDialog = (Application.Current as App)?.Services.GetRequiredService<Views.FinePaymentDialog>();
                fineDialog!.DataContext = fineVm;
                if (fineDialog.ShowDialog() == true)
                {
                    LoadOverdueBooks();
                    MessageBox.Show("Fine processed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No fine to apply for this record.", "No Fine", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}