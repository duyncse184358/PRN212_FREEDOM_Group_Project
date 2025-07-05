using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository repository;
        public BorrowingService(IBorrowingRepository repository) => this.repository = repository;

        public List<Borrowing> GetAllBorrowings() => repository.GetAll();
        public List<Borrowing> GetBorrowingsByPatron(int patronId) => repository.GetByPatronId(patronId);
        public void BorrowBook(Borrowing b) => repository.Add(b);
        public void ReturnBook(int borrowingId) => repository.MarkReturned(borrowingId);
    }
}
