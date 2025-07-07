using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using NguyênChiDuyWPF.Commands;
using NguyênChiDuyWPF.Models; // Cần cho BorrowingDisplayModel
using Services;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;
using LibraryWpfApp.ViewModels;
using LibraryWpfApp;

namespace NguyênChiDuyWPF.ViewModels
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
                _borrowingService.ReturnBook(SelectedBorrowingInfo.BorrowingID);
                MessageBox.Show("Book returned successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                if (SelectedBorrowingInfo.FineAmount > 0 && !SelectedBorrowingInfo.IsFinePaid)
                {
                    if (MessageBox.Show($"Book was overdue. Fine amount: {SelectedBorrowingInfo.FineAmount:C}. Process fine payment now?", "Overdue Fine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var fineVm = (Application.Current as App)?.Services.GetRequiredService<ViewModels.FinePaymentDialogViewModel>();
                        fineVm!.Setup(SelectedBorrowingInfo.BorrowingID,
                                      SelectedBorrowingInfo.PatronID,
                                      SelectedBorrowingInfo.FineAmount,
                                      SelectedBorrowingInfo.PatronName);

                        var fineDialog = (Application.Current as App)?.Services.GetRequiredService<Views.FinePaymentDialog>();
                        fineDialog!.DataContext = fineVm;
                        fineDialog.ShowDialog();
                    }
                }

                Application.Current.Windows.OfType<LibraryWpfApp.Views.ReturnBookDialog>().FirstOrDefault(w => w.DataContext == this)?.DialogResult = true;
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