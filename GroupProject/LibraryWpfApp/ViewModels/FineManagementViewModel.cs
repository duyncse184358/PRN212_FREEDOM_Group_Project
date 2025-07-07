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
using System.Windows;

namespace LibraryWpfApp.ViewModels
{
    public class FineManagementViewModel : BaseViewModel
    {
        private readonly IFineService _fineService;
        private readonly IPatronService _patronService;

        public ObservableCollection<FineDisplayModel> Fines { get; set; } = new();
        public FineDisplayModel? SelectedFine { get; set; }
        public string SearchKeyword { get; set; } = ""; // Search by Patron Name

        public ICommand SearchCommand { get; }
        public ICommand MarkPaidCommand { get; }
        public ICommand ViewAllCommand { get; }

        public FineManagementViewModel(IFineService fineService, IPatronService patronService)
        {
            _fineService = fineService;
            _patronService = patronService;

            SearchCommand = new RelayCommand(Search);
            MarkPaidCommand = new RelayCommand(MarkPaid);
            ViewAllCommand = new RelayCommand(LoadFines);

            LoadFines();
        }

        private void LoadFines()
        {
            Fines.Clear();
            var allFines = _fineService.GetAllFines();
            foreach (var f in allFines)
            {
                var patron = _patronService.GetPatronById(f.PatronId ?? 0);
                Fines.Add(new FineDisplayModel
                {
                    FineID = f.FineId,
                    BorrowingID = f.BorrowingId ?? 0,
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    Amount = f.FineAmount ?? 0M,
                    IsPaid = f.Paid ?? false,
                    FineDate = f.FineDate ?? DateTime.MinValue
                });
            }
        }

        private void Search()
        {
            Fines.Clear();
            var allFines = _fineService.GetAllFines();
            var filtered = allFines.Where(f =>
                _patronService.GetPatronById(f.PatronId ?? 0)?.FullName?.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) == true
            ).ToList();

            foreach (var f in filtered)
            {
                var patron = _patronService.GetPatronById(f.PatronId ?? 0);
                Fines.Add(new FineDisplayModel
                {
                    FineID = f.FineId,
                    BorrowingID = f.BorrowingId ?? 0,
                    PatronName = patron?.FullName ?? "Unknown Patron",
                    Amount = f.FineAmount ?? 0M,
                    IsPaid = f.Paid ?? false,
                    FineDate = f.FineDate ?? DateTime.MinValue
                });
            }
        }

        private void MarkPaid()
        {
            if (SelectedFine == null)
            {
                MessageBox.Show("Please select a fine to mark as paid.", "No Fine Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedFine.IsPaid)
            {
                MessageBox.Show("This fine has already been paid.", "Already Paid", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                _fineService.PayFine(SelectedFine.FineID);
                LoadFines();
                MessageBox.Show("Fine marked as paid successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
