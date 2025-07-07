using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Models;
using Services;
using System.Windows.Input;

namespace LibraryWpfApp.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IFineService _fineService;

        public ObservableCollection<BorrowingDisplayModel> BorrowingsReport { get; set; } = new();
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-30);
        public DateTime ToDate { get; set; } = DateTime.Today;

        public int TotalBorrowedBooks => BorrowingsReport.Count(b => b.Status == "Borrowed" || b.Status == "Overdue");
        public int TotalOverdueBooks => BorrowingsReport.Count(b => b.Status == "Overdue");
        public decimal TotalFinesAmount => BorrowingsReport.Sum(b => b.FineAmount);


        public ICommand GenerateReportCommand { get; }

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
            BorrowingsReport.Clear();
            var result = _borrowingService.GetAllBorrowings()
                // ĐÃ SỬA LỖI: Bỏ .Date cho DateOnly và so sánh trực tiếp
                .Where(b => b.BorrowDate >= DateOnly.FromDateTime(FromDate) && b.BorrowDate <= DateOnly.FromDateTime(ToDate))
                .OrderByDescending(b => b.BorrowDate).ToList();

            foreach (var b in result)
            {
                var book = _bookService.GetBookById(b.BookId ?? 0);
                var patron = _patronService.GetPatronById(b.PatronId ?? 0);
                var fine = _fineService.GetAllFines().FirstOrDefault(f => f.BorrowingId == b.BorrowingId);

                BorrowingsReport.Add(new BorrowingDisplayModel
                {
                    BorrowingID = b.BorrowingId,
                    BookTitle = book?.Title ?? "Unknown Book",
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    BorrowDate = b.BorrowDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    DueDate = b.DueDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    ReturnDate = b.ReturnDate?.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    Status = b.Status ?? "Unknown",
                    FineAmount = fine?.FineAmount ?? 0M,
                    IsFinePaid = fine?.Paid ?? false
                });
            }
            OnPropertyChanged(nameof(TotalBorrowedBooks));
            OnPropertyChanged(nameof(TotalOverdueBooks));
            OnPropertyChanged(nameof(TotalFinesAmount));
        }
    }
}
