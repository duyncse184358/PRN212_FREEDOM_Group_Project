using BusinessObject;
using Reponsitories;
using Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _repo;
        private readonly IFineService _fineService;

        public BorrowingService(IBorrowingRepository repo, IFineService fineService)
        {
            _repo = repo;
            _fineService = fineService;
        }

        public List<Borrowing> GetAllBorrowings() => _repo.GetAll();
        public Borrowing? GetBorrowingById(int id) => _repo.GetById(id);
        public List<Borrowing> GetBorrowingsByPatron(int patronId) => _repo.GetByPatronId(patronId);
        public void BorrowBook(Borrowing borrowing) => _repo.Add(borrowing);
        public void ReturnBook(int borrowingId)
        {
            var borrowing = _repo.GetById(borrowingId);
            if (borrowing != null)
            {
                borrowing.ReturnDate = DateTime.Now;
                borrowing.IsReturned = true;
                borrowing.Status = "Returned";
                _repo.Update(borrowing);

                decimal fineAmount = CalculateFine(borrowing);
                if (fineAmount > 0)
                {
                    var newFine = new BusinessObject.Fine
                    {
                        BorrowingId = borrowing.BorrowingId,
                        PatronId = borrowing.PatronId ?? 0,
                        FineAmount = fineAmount,
                        Paid = false,
                        FineDate = DateTime.Now
                    };
                    _fineService.AddFine(newFine);
                }
            }
        }
        public List<Borrowing> GetOverdueBorrowings() => _repo.GetAll().Where(b => (b.Status == "Borrowed" || b.Status == "Overdue") && (b.IsReturned == null || b.IsReturned == false) && b.DueDate < DateTime.Now).ToList();

        public bool IsOverdue(Borrowing borrowing)
        {
            return (borrowing.Status == "Borrowed" || borrowing.Status == "Overdue") && (borrowing.IsReturned == null || borrowing.IsReturned == false) && borrowing.DueDate < DateTime.Now;
        }

        public decimal CalculateFine(Borrowing borrowing)
        {
            if ((borrowing.Status != "Returned" && !IsOverdue(borrowing)) || !borrowing.ReturnDate.HasValue) return 0M;

            DateTime actualReturnDate = borrowing.ReturnDate ?? DateTime.Now;

            if (actualReturnDate.Date > borrowing.DueDate.Date)
            {
                TimeSpan daysOverdue = actualReturnDate.Date - borrowing.DueDate.Date;
                decimal fineRatePerDay = 0.5M;
                return daysOverdue.Days * fineRatePerDay;
            }
            return 0M;
        }
    }
}