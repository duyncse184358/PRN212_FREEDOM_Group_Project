using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.ViewModels;

namespace LibraryWpfApp.Models
{
    public class BorrowingDisplayModel : BaseViewModel
    {
        private int _borrowingID;
        public int BorrowingID
        {
            get => _borrowingID;
            set => SetProperty(ref _borrowingID, value);
        }

        private string _bookTitle = string.Empty;
        public string BookTitle
        {
            get => _bookTitle;
            set => SetProperty(ref _bookTitle, value);
        }

        private int _patronID;
        public int PatronID
        {
            get => _patronID;
            set => SetProperty(ref _patronID, value);
        }

        private string _patronName = string.Empty;
        public string PatronName
        {
            get => _patronName;
            set => SetProperty(ref _patronName, value);
        }

        private DateTime _borrowDate;
        public DateTime BorrowDate
        {
            get => _borrowDate;
            set => SetProperty(ref _borrowDate, value);
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        private DateTime? _returnDate;
        public DateTime? ReturnDate
        {
            get => _returnDate;
            set => SetProperty(ref _returnDate, value);
        }

        private string _status = string.Empty;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private decimal _fineAmount;
        public decimal FineAmount
        {
            get => _fineAmount;
            set => SetProperty(ref _fineAmount, value);
        }

        private bool _isFinePaid;
        public bool IsFinePaid
        {
            get => _isFinePaid;
            set => SetProperty(ref _isFinePaid, value);
        }
    }
}
