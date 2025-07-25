using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;
using Microsoft.EntityFrameworkCore;

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
        public ICommand ViewOverdueCommand { get; }
        public ICommand CalculateAndAddFineCommand { get; }
        public ICommand MarkLostCommand { get; }

        //public ICommand ReturnBookCommand { get; }
        public ICommand MarkDamagedCommand { get; }

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
            CalculateAndAddFineCommand = new RelayCommand(CalculateAndAddFine);
            MarkLostCommand = new RelayCommand(obj =>
            {
                if (obj is BorrowingDisplayModel item)
                    OnMarkLost(item);
            });
            MarkDamagedCommand = new RelayCommand(obj =>
            {
                if (obj is BorrowingDisplayModel item)
                    OnMarkDamaged(item);
            });
            //ReturnBookCommand = new RelayCommand(ReturnBook);
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
                    BorrowDate = b.BorrowDate.ToDateTime(TimeOnly.MinValue),
                    DueDate = b.DueDate.ToDateTime(TimeOnly.MinValue),
                    ReturnDate = b.ReturnDate?.ToDateTime(TimeOnly.MinValue),
                    Status = b.Status ?? "Unknown",
                    FineAmount = fine?.FineAmount ?? 0M,
                    IsFinePaid = fine?.Paid ?? true,
                    PatronID = b.PatronId ?? 0
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

        private void OnMarkLost(BorrowingDisplayModel item)
        {
            // Nếu sách đã là "Lost", cho phép phục hồi về trạng thái thường
            if (item.Status == "Lost")
            {
                var confirm = MessageBox.Show(
     "This book has been marked as lost. Do you want to restore it to normal status?",
     "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Phục hồi trạng thái sách
                        _borrowingService.MarkBookCopyAsNormal(item.BorrowingID);

                        // Xóa tất cả các khoản phạt chưa thanh toán liên quan đến BorrowingID này
                        var finesToDelete = _fineService.GetAllFines()
                            .Where(f => f.BorrowingId == item.BorrowingID && (f.Paid == null || f.Paid == false))
                            .ToList();

                        foreach (var fine in finesToDelete)
                        {
                            _fineService.DeleteFine(fine.FineId);
                        }

                        LoadBorrowings();
                        MessageBox.Show(
                             $"The book '{item.BookTitle}' has been restored to normal status and the related fine has been removed.",
                             "Success", MessageBoxButton.OK, MessageBoxImage.Information
                         );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                             $"Error while restoring: {ex.Message}",
                             "Error", MessageBoxButton.OK, MessageBoxImage.Error
                         );

                    }
                }
                return;
            }

            // Nếu chưa phải trạng thái "Lost", tiến hành đánh dấu mất và tạo khoản phạt nếu cần
            var borrowing = _borrowingService.GetBorrowingById(item.BorrowingID);
            if (borrowing == null)
            {
                MessageBox.Show("Borrowing record not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmLost = MessageBox.Show(
                $"Are you sure you want to mark the book '{item.BookTitle}' as LOST?",
                 "Confirm Mark as Lost", MessageBoxButton.YesNo, MessageBoxImage.Warning);


            if (confirmLost == MessageBoxResult.No)
                return;

            try
            {
                _borrowingService.MarkBookCopyAsLost(borrowing.BorrowingId);

                // Nếu chưa có khoản phạt cho phiếu này, tạo mới
                var book = _bookService.GetBookById(borrowing.BookId ?? 0);
                var existingFine = _fineService.GetAllFines()
                    .FirstOrDefault(f =>
                        f.BorrowingId == borrowing.BorrowingId
                        && f.PatronId == borrowing.PatronId
                        && (f.Paid == null || f.Paid == false)
                    );

                if (book != null && existingFine == null)
                {
                    var fine = new BusinessObject.Fine
                    {
                        BorrowingId = borrowing.BorrowingId,
                        PatronId = borrowing.PatronId,
                        FineAmount = book.Price,
                        FineType = "Lost",
                        Paid = false,
                        FineDate = DateTime.Now
                    };
                    _fineService.AddFine(fine);
                }

                LoadBorrowings();
                MessageBox.Show(
                  $"The book '{item.BookTitle}' has been marked as lost and a fine has been issued.",
                  "Success", MessageBoxButton.OK, MessageBoxImage.Information
              );

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                      $"Error while marking as lost: {ex.Message}",
                      "Error", MessageBoxButton.OK, MessageBoxImage.Error
                  );

            }
        }

        private void OnMarkDamaged(BorrowingDisplayModel item)
        {
            if (item.Status == "Damaged")
            {
                MessageBox.Show("This book has been marked as damaged.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var b = _borrowingService.GetBorrowingById(item.BorrowingID);
            if (b == null)
            {
                MessageBox.Show("Borrowing record not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show(
                          $"Are you sure you want to mark the book '{item.BookTitle}' as DAMAGED?",
                            "Confirm Mark as Damaged", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)

            {
                return;
            }

            try
            {
                _borrowingService.MarkBookCopyAsDamaged(b.BorrowingId);

                // Tạo khoản phạt nếu chưa có
                var book = _bookService.GetBookById(b.BookId ?? 0);
                var existingFine = _fineService.GetAllFines()
                    .FirstOrDefault(f => f.BorrowingId == b.BorrowingId && f.PatronId == b.PatronId && (f.Paid == null || f.Paid == false));
                if (book != null && existingFine == null)
                {
                    var fine = new BusinessObject.Fine
                    {
                        BorrowingId = b.BorrowingId,
                        PatronId = b.PatronId,
                        FineAmount = book.Price,
                        FineType = "Damaged",
                        Paid = false,
                        FineDate = DateTime.Now
                    };
                    _fineService.AddFine(fine);
                }

                LoadBorrowings();
                MessageBox.Show($"The book \"{item.BookTitle}\" has been marked as damaged and a fine has been created.",
                "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while marking as damaged: {ex.Message}",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}