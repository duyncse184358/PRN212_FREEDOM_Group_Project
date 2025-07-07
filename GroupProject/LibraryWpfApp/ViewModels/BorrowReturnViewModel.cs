using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;

namespace LibraryWpfApp.ViewModels
{
    public class BorrowReturnViewModel : BaseViewModel
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IFineService _fineService;

        public ObservableCollection<BorrowingDisplayModel> Borrowings { get; set; } = new();
        public BorrowingDisplayModel? SelectedBorrowing { get; set; }

        public string SearchKeyword { get; set; } = "";

        public ICommand SearchCommand { get; }
        public ICommand MarkReturnedCommand { get; }
        public ICommand ViewOverdueCommand { get; }
        public ICommand CalculateAndAddFineCommand { get; }

        public BorrowReturnViewModel()
        {
           
        }
        public BorrowReturnViewModel(IBorrowingService borrowingService, IBookService bookService, IPatronService patronService, IFineService fineService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
            _patronService = patronService;
            _fineService = fineService;

            SearchCommand = new RelayCommand(Search);
            MarkReturnedCommand = new RelayCommand(MarkReturned);
            ViewOverdueCommand = new RelayCommand(ViewOverdue);
            CalculateAndAddFineCommand = new RelayCommand(CalculateAndAddFine);

            LoadBorrowings();
        }

        private void LoadBorrowings()
        {
            Borrowings.Clear();
            var allBorrowings = _borrowingService.GetAllBorrowings();
            foreach (var b in allBorrowings)
            {
                var book = _bookService.GetBookById(b.BookId ?? 0);
                var patron = _patronService.GetPatronById(b.PatronId ?? 0);
                var fine = _fineService.GetAllFines().FirstOrDefault(f => f.BorrowingId == b.BorrowingId && (f.Paid == null || f.Paid == false));

                Borrowings.Add(new BorrowingDisplayModel
                {
                    BorrowingID = b.BorrowingId,
                    BookTitle = book?.Title ?? "Unknown Book",
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    BorrowDate = b.BorrowDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    DueDate = b.DueDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    ReturnDate = b.ReturnDate?.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    Status = b.Status ?? "Unknown",
                    FineAmount = fine?.FineAmount ?? 0M,
                    IsFinePaid = fine?.Paid ?? true, // Sử dụng IsFinePaid
                    PatronID = b.PatronId ?? 0 // Thêm PatronID
                });
            }
        }

        private void Search()
        {
            Borrowings.Clear();
            var allBorrowings = _borrowingService.GetAllBorrowings();
            var filtered = allBorrowings.Where(b =>
                (_bookService.GetBookById(b.BookId ?? 0)?.Title?.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) == true) ||
                (_patronService.GetPatronById(b.PatronId ?? 0)?.FullName?.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) == true)
            ).ToList();

            foreach (var b in filtered)
            {
                var book = _bookService.GetBookById(b.BookId ?? 0);
                var patron = _patronService.GetPatronById(b.PatronId ?? 0);
                var fine = _fineService.GetAllFines().FirstOrDefault(f => f.BorrowingId == b.BorrowingId && (f.Paid == null || f.Paid == false));

                Borrowings.Add(new BorrowingDisplayModel
                {
                    BorrowingID = b.BorrowingId,
                    BookTitle = book?.Title ?? "Unknown Book",
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    PatronID = b.PatronId ?? 0, // Thêm PatronID
                    BorrowDate = b.BorrowDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    DueDate = b.DueDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    ReturnDate = b.ReturnDate?.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    Status = b.Status ?? "Unknown",
                    FineAmount = fine?.FineAmount ?? 0M,
                    IsFinePaid = fine?.Paid ?? true // Sử dụng IsFinePaid
                });
            }
        }

        private void MarkReturned()
        {
            if (SelectedBorrowing == null)
            {
                MessageBox.Show("Please select a borrowing record to mark as returned.", "No Record Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBorrowing.Status == "Returned")
            {
                MessageBox.Show("This book has already been returned.", "Already Returned", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                _borrowingService.ReturnBook(SelectedBorrowing.BorrowingID);
                LoadBorrowings();

                MessageBox.Show("Book marked as returned successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                var originalBorrowing = _borrowingService.GetBorrowingById(SelectedBorrowing.BorrowingID);
                if (originalBorrowing != null)
                {
                    decimal fineAmount = _borrowingService.CalculateFine(originalBorrowing);
                    if (fineAmount > 0)
                    {
                        if (MessageBox.Show($"Book is overdue. Fine amount: {fineAmount:C}. Do you want to process fine payment now?", "Overdue Fine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            var fineVm = (Application.Current as App)?.Services.GetRequiredService<FinePaymentDialogViewModel>();
                            fineVm!.Setup(originalBorrowing.BorrowingId,
                                          originalBorrowing.PatronId ?? 0,
                                          fineAmount,
                                          _patronService.GetPatronById(originalBorrowing.PatronId ?? 0)?.FullName ?? "Unknown Patron");

                            var fineDialog = (Application.Current as App)?.Services.GetRequiredService<Views.FinePaymentDialog>();
                            fineDialog!.DataContext = fineVm;
                            fineDialog.ShowDialog();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Return Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewOverdue()
        {
            (Application.Current as App)?.Services.GetRequiredService<Views.OverdueBooksWindow>()?.Show();
        }

        private void CalculateAndAddFine()
        {
            if (SelectedBorrowing == null)
            {
                MessageBox.Show("Please select a borrowing record.", "No Record Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedBorrowing.Status == "Returned" && SelectedBorrowing.IsFinePaid)
            {
                MessageBox.Show("This record is already returned and fine paid (if applicable).", "No Fine Needed", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (SelectedBorrowing.Status == "Borrowed" && !_borrowingService.IsOverdue(_borrowingService.GetBorrowingById(SelectedBorrowing.BorrowingID)!))
            {
                MessageBox.Show("This book is not overdue yet.", "Not Overdue", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var originalBorrowing = _borrowingService.GetBorrowingById(SelectedBorrowing.BorrowingID);
            if (originalBorrowing == null) return;

            decimal fineAmount = _borrowingService.CalculateFine(originalBorrowing);

            if (fineAmount > 0)
            {
                var fineVm = (Application.Current as App)?.Services.GetRequiredService<FinePaymentDialogViewModel>();
                fineVm!.Setup(originalBorrowing.BorrowingId,
                               originalBorrowing.PatronId ?? 0,
                               fineAmount,
                               _patronService.GetPatronById(originalBorrowing.PatronId ?? 0)?.FullName ?? "Unknown Patron");

                var fineDialog = (Application.Current as App)?.Services.GetRequiredService<Views.FinePaymentDialog>();
                fineDialog!.DataContext = fineVm;
                if (fineDialog.ShowDialog() == true)
                {
                    LoadBorrowings();
                    MessageBox.Show("Fine processed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No fine to apply for this borrowing record.", "No Fine", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}