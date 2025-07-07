using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using LibraryWpfApp.Models;
using Services;

namespace LibraryWpfApp.ViewModels
{
    public class PatronBorrowingHistoryViewModel : BaseViewModel
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;
        private readonly IPatronService _patronService;
        private readonly IFineService _fineService;

        public Patron Patron { get; set; }
        public ObservableCollection<BorrowingDisplayModel> BorrowingHistory { get; set; } = new();

        public PatronBorrowingHistoryViewModel(int patronId, IBorrowingService borrowingService, IBookService bookService, IPatronService patronService, IFineService fineService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
            _patronService = patronService;
            _fineService = fineService;

            Patron = _patronService.GetPatronById(patronId) ?? new BusinessObject.Patron();
            LoadBorrowingHistory();
        }

        private void LoadBorrowingHistory()
        {
            BorrowingHistory.Clear();
            if (Patron.PatronId == 0)
            {
                Console.WriteLine("Patron ID is 0, cannot load borrowing history.");
                return;
            }

            var history = _borrowingService.GetBorrowingsByPatron(Patron.PatronId);
            foreach (var b in history)
            {
                var book = _bookService.GetBookById(b.BookId ?? 0);
                var fine = _fineService.GetAllFines().FirstOrDefault(f => f.BorrowingId == b.BorrowingId);

                BorrowingHistory.Add(new BorrowingDisplayModel
                {
                    BorrowingID = b.BorrowingId,
                    BookTitle = book?.Title ?? "Unknown Book",
                    PatronName = Patron.FullName ?? "Unknown Patron",
                    BorrowDate = b.BorrowDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    DueDate = b.DueDate.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    ReturnDate = b.ReturnDate?.ToDateTime(TimeOnly.MinValue), // CHUYỂN ĐỔI TẠI ĐÂY
                    Status = b.Status ?? "Unknown",
                    FineAmount = fine?.FineAmount ?? 0M,
                    IsFinePaid = fine?.Paid ?? false
                });
            }
        }
    }
}
