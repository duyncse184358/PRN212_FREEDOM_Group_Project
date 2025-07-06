using BusinessObject;
using System.Collections.Generic;
using System;

namespace Services
{
    public interface IBorrowingService
    {
        List<Borrowing> GetAllBorrowings();
        Borrowing? GetBorrowingById(int id);
        List<Borrowing> GetBorrowingsByPatron(int patronId);
        void BorrowBook(Borrowing borrowing);
        void ReturnBook(int borrowingId);
        List<Borrowing> GetOverdueBorrowings();
        bool IsOverdue(Borrowing borrowing);
        decimal CalculateFine(Borrowing borrowing);
    }
}