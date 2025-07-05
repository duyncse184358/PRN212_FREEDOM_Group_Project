using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Services
{
    public interface IBorrowingService
    {
        List<Borrowing> GetAllBorrowings();
        List<Borrowing> GetBorrowingsByPatron(int patronId);
        void BorrowBook(Borrowing b);
        void ReturnBook(int borrowingId);
    }
}
