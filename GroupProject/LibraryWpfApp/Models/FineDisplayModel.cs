using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.ViewModels;

namespace LibraryWpfApp.Models
{
    public class FineDisplayModel : BaseViewModel
    {
        private int _fineID;
        public int FineID
        {
            get => _fineID;
            set => SetProperty(ref _fineID, value);
        }

        private int _borrowingID;
        public int BorrowingID
        {
            get => _borrowingID;
            set => SetProperty(ref _borrowingID, value);
        }

        private string _patronName = string.Empty;
        public string PatronName
        {
            get => _patronName;
            set => SetProperty(ref _patronName, value);
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        private bool _isPaid;
        public bool IsPaid
        {
            get => _isPaid;
            set => SetProperty(ref _isPaid, value);
        }

        private DateTime _fineDate;
        public DateTime FineDate
        {
            get => _fineDate;
            set => SetProperty(ref _fineDate, value);
        }

        private string _fineType = string.Empty;
        public string FineType
        {
            get => _fineType;
            set => SetProperty(ref _fineType, value);
        }

        private int? _lateDays;
        public int? LateDays
        {
            get => _lateDays;
            set => SetProperty(ref _lateDays, value);
        }

        private DateTime? _fineDueDate;
        public DateTime? FineDueDate
        {
            get => _fineDueDate;
            set => SetProperty(ref _fineDueDate, value);
        }
    }
}
