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
            set => SetProperty(ref _fromDate, value);
        }

        private DateTime _toDate = DateTime.Today;
        public DateTime ToDate
        {
            get => _toDate;
            set => SetProperty(ref _toDate, value);
        }

        public string TotalBorrowedBooksText => (BorrowingsReport?.Count(b => b.Status == "Borrowed" || b.Status == "Overdue") ?? 0).ToString();
        public string TotalOverdueBooksText => (BorrowingsReport?.Count(b => b.Status == "Overdue") ?? 0).ToString();
        public string TotalFinesAmountText =>
    string.Format("{0:N0} đ", BorrowingsReport?.Sum(b => b.FineAmount) ?? 0M);


        public ICommand GenerateReportCommand { get; }

        // Constructor mặc định (public parameterless constructor) cho XAML
        public ReportViewModel() : this(
            (Application.Current as App)?.Services.GetRequiredService<IBorrowingService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IBookService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IPatronService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IFineService>()!
        )
        {
        }

        public ReportViewModel(IBorrowingService borrowingService, IBookService bookService, IPatronService patronService, IFineService fineService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
            _patronService = patronService;
            _fineService = fineService;

            GenerateReportCommand = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("GenerateReportCommand executed");
                GenerateReport();
            });

            // Không auto load, để user click Generate Report
        }

        public void GenerateReport()
        {
            try
            {
                if (_borrowingService == null)
                {
                    MessageBox.Show("Borrowing service not available. Cannot generate report.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                BorrowingsReport.Clear();
                var allBorrowings = _borrowingService.GetAllBorrowings();

                if (allBorrowings == null)
                {
                    MessageBox.Show("No borrowing data available.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = allBorrowings
                    .Where(b => b.BorrowDate >= DateOnly.FromDateTime(FromDate) && b.BorrowDate <= DateOnly.FromDateTime(ToDate))
                    .OrderByDescending(b => b.BorrowingId).ToList();

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

                MessageBox.Show($"Report generated successfully! Found {BorrowingsReport.Count} borrowing records.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating report: {ex.Message}\n\nPlease check the data and try again.", "Report Error", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Diagnostics.Debug.WriteLine($"ERROR in GenerateReport: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
    }
}
