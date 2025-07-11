using BusinessObject;
using Services;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;

namespace LibraryWpfApp.ViewModels
{
    public class ReturnBookDialogViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        private readonly IBorrowingService _borrowingService;
        private readonly IPatronService _patronService;
        private readonly IFineService _fineService;

        public string IDInput { get; set; } = string.Empty;
        public BorrowingDisplayModel? SelectedBorrowingInfo { get; set; }

        public ICommand SearchBorrowingCommand { get; }
        public ICommand ConfirmReturnCommand { get; }

        public ReturnBookDialogViewModel()
        {
            
        }

        public ReturnBookDialogViewModel(IBookService bookService, IBorrowingService borrowingService, IPatronService patronService, IFineService fineService)
        {
            _bookService = bookService;
            _borrowingService = borrowingService;
            _patronService = patronService;
            _fineService = fineService;

            SearchBorrowingCommand = new RelayCommand(SearchBorrowing);
            ConfirmReturnCommand = new RelayCommand(ConfirmReturn);
        }

        private void SearchBorrowing()
        {
            if (!int.TryParse(IDInput, out int id))
            {
                MessageBox.Show("Please enter a valid Book ID or Borrowing ID.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Borrowing? borrowing = _borrowingService.GetBorrowingById(id);

            if (borrowing == null)
            {
                borrowing = _borrowingService.GetAllBorrowings()
                                .FirstOrDefault(b => b.BookId == id && (b.IsReturned == null || b.IsReturned == false));
            }

            if (borrowing != null && (borrowing.IsReturned == null || borrowing.IsReturned == false)) // Only show active borrowed
            {
                var book = _bookService.GetBookById(borrowing.BookId ?? 0);
                var patron = _patronService.GetPatronById(borrowing.PatronId ?? 0);

                SelectedBorrowingInfo = new BorrowingDisplayModel
                {
                    BorrowingID = borrowing.BorrowingId,
                    BookTitle = book?.Title ?? "Unknown Book",
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    PatronID = borrowing.PatronId ?? 0, // Thêm PatronID
                    BorrowDate = borrowing.BorrowDate.ToDateTime(TimeOnly.MinValue), // ĐÃ SỬA LỖI: Chuyển DateOnly sang DateTime
                    DueDate = borrowing.DueDate.ToDateTime(TimeOnly.MinValue), // ĐÃ SỬA LỖI: Chuyển DateOnly sang DateTime
                    ReturnDate = borrowing.ReturnDate?.ToDateTime(TimeOnly.MinValue), // ĐÃ SỬA LỖI: Chuyển DateOnly? sang DateTime?
                    Status = borrowing.Status ?? "Unknown",
                    FineAmount = _borrowingService.CalculateFine(borrowing),
                    IsFinePaid = _fineService.GetAllFines().Any(f => f.BorrowingId == borrowing.BorrowingId && (f.Paid == true))
                };
            }
            else
            {
                SelectedBorrowingInfo = null;
                MessageBox.Show("No active borrowing record found for this ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            OnPropertyChanged(nameof(SelectedBorrowingInfo));
        }

        private void ConfirmReturn()
        {
            if (SelectedBorrowingInfo == null)
            {
                MessageBox.Show("Please search and select a borrowing record first.", "No Record Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Lấy thông tin phiếu mượn thực tế
                var borrowing = _borrowingService.GetBorrowingById(SelectedBorrowingInfo.BorrowingID);
                if (borrowing == null)
                {
                    MessageBox.Show("Borrowing record not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Đặt ngày trả và trạng thái trả
                borrowing.IsReturned = true;
                borrowing.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                borrowing.Status = "Returned";
                _borrowingService.UpdateBorrowing(borrowing);

                // Kiểm tra quá hạn
                if (borrowing.ReturnDate > borrowing.DueDate)
                {
                    int lateDays = (borrowing.ReturnDate.Value.ToDateTime(TimeOnly.MinValue) - borrowing.DueDate.ToDateTime(TimeOnly.MinValue)).Days;
                    decimal fineAmount = lateDays * 5000;

                    // Kiểm tra đã có khoản phạt chưa (tránh tạo trùng)
                    bool alreadyFined = _fineService.GetAllFines().Any(f => f.BorrowingId == borrowing.BorrowingId && f.FineType == "LateReturn");
                    if (!alreadyFined)
                    {
                        var fine = new Fine
                        {
                            BorrowingId = borrowing.BorrowingId,
                            PatronId = borrowing.PatronId,
                            FineAmount = fineAmount,
                            Paid = false,
                            FineDate = DateTime.Now,
                            FineDueDate = DateTime.Now.AddDays(3),
                            FineType = "LateReturn",
                            LateDays = lateDays
                        };
                        _fineService.AddFine(fine);
                    }

                    MessageBox.Show($"Trả sách trễ {lateDays} ngày, bạn bị phạt {fineAmount:N0} đ.", "Phạt trả trễ", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Xử lý nộp phạt nếu có
                var unpaidFines = _fineService.GetAllFines().Where(f => f.BorrowingId == borrowing.BorrowingId && f.Paid == false).ToList();
                if (unpaidFines.Any())
                {
                    decimal totalFine = unpaidFines.Sum(f => f.FineAmount ?? 0);
                    if (MessageBox.Show($"Tổng tiền phạt: {totalFine:C}. Bạn có muốn thanh toán ngay không?", "Tiền phạt", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var fineVm = (Application.Current as App)?.Services.GetRequiredService<ViewModels.FinePaymentDialogViewModel>();
                        fineVm!.Setup(borrowing.BorrowingId, borrowing.PatronId ?? 0, totalFine, SelectedBorrowingInfo.PatronName);

                        var fineDialog = (Application.Current as App)?.Services.GetRequiredService<Views.FinePaymentDialog>();
                        fineDialog!.DataContext = fineVm;
                        fineDialog.ShowDialog();
                    }
                }

                // Đóng dialog
                var dialog = Application.Current.Windows.OfType<Views.ReturnBookDialog>().FirstOrDefault(w => w.DataContext == this);
                if (dialog != null)
                {
                    dialog.DialogResult = true;
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
    }
}