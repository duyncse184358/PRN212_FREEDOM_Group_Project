using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;
using Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IFineService _fineService;

        public ObservableCollection<BorrowingDisplayModel> BorrowingsReport { get; set; } = new();

        private DateTime _fromDate = DateTime.Today.AddDays(-30);
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                if (SetProperty(ref _fromDate, value))
                {
                    GenerateReport();
                }
            }
        }

        private DateTime _toDate = DateTime.Today;
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                if (SetProperty(ref _toDate, value))
                {
                    GenerateReport();
                }
            }
        }

        public string TotalBorrowedBooksText => (BorrowingsReport?.Count(b => b.Status == "Borrowed" || b.Status == "Overdue") ?? 0).ToString();
        public string TotalOverdueBooksText => (BorrowingsReport?.Count(b => b.Status == "Overdue") ?? 0).ToString();
        public string TotalFinesAmountText => (BorrowingsReport?.Sum(b => b.FineAmount) ?? 0M).ToString("C");

        public ICommand GenerateReportCommand { get; }

        // Constructor mặc định (public parameterless constructor) cho XAML
        public ReportViewModel()
        {
        }

        public ReportViewModel(IBorrowingService borrowingService, IBookService bookService, IPatronService patronService, IFineService fineService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
            _patronService = patronService;
            _fineService = fineService;

            GenerateReportCommand = new RelayCommand(GenerateReport);
            GenerateReport();
        }

        private void GenerateReport()
        {
            try
            {
                BorrowingsReport.Clear();
                var allBorrowings = _borrowingService?.GetAllBorrowings();

                if (allBorrowings == null)
                {
                    Console.WriteLine("Warning: _borrowingService.GetAllBorrowings() returned null or service is null.");
                    return;
                }

                var result = allBorrowings
                    .Where(b => b.BorrowDate >= DateOnly.FromDateTime(FromDate) && b.BorrowDate <= DateOnly.FromDateTime(ToDate))
                    .OrderByDescending(b => b.BorrowDate).ToList();

                foreach (var b in result)
                {
                    var book = _bookService?.GetBookById(b.BookId ?? 0);
                    var patron = _patronService?.GetPatronById(b.PatronId ?? 0);
                    var fine = _fineService?.GetAllFines().FirstOrDefault(f => f.BorrowingId == b.BorrowingId);

                    BorrowingsReport.Add(new BorrowingDisplayModel
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
                        IsFinePaid = fine?.Paid ?? false
                    });
                }
                // Thông báo cho UI rằng các thuộc tính đã thay đổi
                OnPropertyChanged(nameof(TotalBorrowedBooksText));
                OnPropertyChanged(nameof(TotalOverdueBooksText));
                OnPropertyChanged(nameof(TotalFinesAmountText));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in GenerateReport: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                MessageBox.Show($"An error occurred while generating report: {ex.Message}\n\nCheck Output Window for details.", "Report Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
