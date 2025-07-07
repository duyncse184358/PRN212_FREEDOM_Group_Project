using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using LibraryWpfApp.Commands;
using Services;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.ViewModels
{
    public class PatronViewModel : BaseViewModel
    {
        private readonly IPatronService _patronService;
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;
        private readonly IFineService _fineService;

        public ObservableCollection<Patron> Patrons { get; set; } = new();
        public Patron? SelectedPatron { get; set; }

        public string SearchKeyword { get; set; } = "";

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ViewBorrowingHistoryCommand { get; }

        public PatronViewModel(IPatronService patronService, IBorrowingService borrowingService, IBookService bookService, IFineService fineService)
        {
            _patronService = patronService;
            _borrowingService = borrowingService;
            _bookService = bookService;
            _fineService = fineService;

            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            ViewBorrowingHistoryCommand = new RelayCommand(ViewBorrowingHistory);

            LoadPatrons();
        }

        private void LoadPatrons()
        {
            Patrons.Clear();
            foreach (var p in _patronService.GetAllPatrons())
                Patrons.Add(p);
        }

        private void Search()
        {
            Patrons.Clear();
            var result = _patronService.SearchPatrons(SearchKeyword);
            foreach (var p in result)
                Patrons.Add(p);
        }

        private void Add()
        {
            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.PatronDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<PatronDialogViewModel>();

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as PatronDialogViewModel;
                if (vm != null)
                {
                    _patronService.AddPatron(vm.Patron);
                    LoadPatrons();
                }
            }
        }

        private void Edit()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron to edit.", "No Patron Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.PatronDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<PatronDialogViewModel>(
                new object[] { SelectedPatron! });

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as PatronDialogViewModel;
                if (vm != null)
                {
                    _patronService.UpdatePatron(vm.Patron);
                    LoadPatrons();
                }
            }
        }

        private void Delete()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron to delete.", "No Patron Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var activeBorrowings = _borrowingService.GetAllBorrowings().Any(b => b.PatronId == SelectedPatron.PatronId && (b.IsReturned == null || b.IsReturned == false));
            if (activeBorrowings)
            {
                MessageBox.Show("Cannot delete patron with active borrowed books.", "Deletion Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete patron '{SelectedPatron.FullName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _patronService.DeletePatron(SelectedPatron.PatronId);
                LoadPatrons();
            }
        }

        private void ViewBorrowingHistory()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron to view their borrowing history.", "No Patron Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var historyWindow = (Application.Current as App)?.Services.GetRequiredService<Views.PatronBorrowingHistoryWindow>();
            historyWindow!.DataContext = (Application.Current as App)?.Services.GetRequiredService<PatronBorrowingHistoryViewModel>(
                new object[] { SelectedPatron.PatronId, _borrowingService, _bookService, _patronService, _fineService });

            historyWindow.Show();
        }
    }
}
