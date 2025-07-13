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

        // THÊM PROPERTY CHO SEARCH TYPE
        private string _selectedSearchType = "BorrowingID";
        public string SelectedSearchType
        {
            get => _selectedSearchType;
            set
            {
                _selectedSearchType = value;
                OnPropertyChanged();
            }
        }

        // CÁC PROPERTY 
        private string _idInput = string.Empty;
        public string IDInput
        {
            get => _idInput;
            set
            {
                _idInput = value;
                OnPropertyChanged();
            }
        }

        private BorrowingDisplayModel? _selectedBorrowingInfo;
        public BorrowingDisplayModel? SelectedBorrowingInfo
        {
            get => _selectedBorrowingInfo;
            set
            {
                _selectedBorrowingInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchBorrowingCommand { get; }
        public ICommand ConfirmReturnCommand { get; }

        public ReturnBookDialogViewModel()
        {
            SearchBorrowingCommand = new RelayCommand(SearchBorrowing);
            ConfirmReturnCommand = new RelayCommand(ConfirmReturn);
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

        //  METHOD SEARCHBORROWING
        private void SearchBorrowing()
        {
            if (string.IsNullOrWhiteSpace(IDInput))
            {
                MessageBox.Show("Please enter a search value.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Borrowing? borrowing = null;

            try
            {
                switch (SelectedSearchType)
                {
                    case "BorrowingID":
                        if (int.TryParse(IDInput, out int borrowingId))
                        {
                            borrowing = _borrowingService.GetBorrowingById(borrowingId);
                            if (borrowing != null && (borrowing.IsReturned == null || borrowing.IsReturned == false))
                            {
                                // Borrowing found and active
                            }
                            else
                            {
                                borrowing = null; // Reset if not active
                            }
                        }
                        break;

                    case "PatronID":
                        if (int.TryParse(IDInput, out int patronId))
                        {
                            borrowing = _borrowingService.GetAllBorrowings()
                                .FirstOrDefault(b => b.PatronId == patronId && (b.IsReturned == null || b.IsReturned == false));
                        }
                        break;

                    case "PatronName":
                        var patron = _patronService.GetAllPatrons()
                            .FirstOrDefault(p => p.FullName.Contains(IDInput, StringComparison.OrdinalIgnoreCase));
                        if (patron != null)
                        {
                            borrowing = _borrowingService.GetAllBorrowings()
                                .FirstOrDefault(b => b.PatronId == patron.PatronId && (b.IsReturned == null || b.IsReturned == false));
                        }
                        break;

                    default:
                        // Fallback to old logic for BorrowingID
                        if (int.TryParse(IDInput, out int id))
                        {
                            borrowing = _borrowingService.GetBorrowingById(id);
                            if (borrowing == null)
                            {
                                borrowing = _borrowingService.GetAllBorrowings()
                                    .FirstOrDefault(b => b.BookId == id && (b.IsReturned == null || b.IsReturned == false));
                            }
                        }
                        break;
                }

                if (borrowing != null && (borrowing.IsReturned == null || borrowing.IsReturned == false))
                {
                    var book = _bookService.GetBookById(borrowing.BookId ?? 0);
                    var patronInfo = _patronService.GetPatronById(borrowing.PatronId ?? 0);

                    SelectedBorrowingInfo = new BorrowingDisplayModel
                    {
                        BorrowingID = borrowing.BorrowingId,
                        BookTitle = book?.Title ?? "Unknown Book",
                        PatronName = patronInfo?.FullName ?? "Unknown Patron",
                        PatronID = borrowing.PatronId ?? 0,
                        BorrowDate = borrowing.BorrowDate.ToDateTime(TimeOnly.MinValue),
                        DueDate = borrowing.DueDate.ToDateTime(TimeOnly.MinValue),
                        ReturnDate = borrowing.ReturnDate?.ToDateTime(TimeOnly.MinValue),
                        Status = borrowing.Status ?? "Unknown",
                        FineAmount = _borrowingService.CalculateFine(borrowing),
                        IsFinePaid = _fineService.GetAllFines().Any(f => f.BorrowingId == borrowing.BorrowingId && (f.Paid == true))
                    };
                }
                else
                {
                    SelectedBorrowingInfo = null;
                    string searchType = SelectedSearchType switch
                    {
                        "BorrowingID" => "Borrowing ID",
                        "PatronID" => "Patron ID",
                        "PatronName" => "Patron Name",
                        _ => "search value"
                    };
                    MessageBox.Show($"No active borrowing record found for this {searchType}.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                SelectedBorrowingInfo = null;
                MessageBox.Show($"Error during search: {ex.Message}", "Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            OnPropertyChanged(nameof(SelectedBorrowingInfo));
        }

        // PHẦN CONFIRMRETURN 
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

