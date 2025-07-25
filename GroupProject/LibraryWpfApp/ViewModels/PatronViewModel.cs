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
        private Patron? _selectedPatron;
        private string _searchKeyword = "";

        public ObservableCollection<Patron> Patrons { get; set; } = new();

        public Patron? SelectedPatron
        {
            get => _selectedPatron;
            set
            {
                _selectedPatron = value;
                OnPropertyChanged();
            }
        }

        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged();
            }
        }

        public bool CanManagePatrons => AppContext.IsAdmin;

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ViewBorrowingHistoryCommand { get; }

        // Constructor mặc định (public parameterless constructor) cho XAML
        public PatronViewModel() : this(
            (Application.Current as App)?.Services.GetRequiredService<IPatronService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IBorrowingService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IBookService>()!,
            (Application.Current as App)?.Services.GetRequiredService<IFineService>()!
        )
        {
        }

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
            if (_patronService == null)
            {
                MessageBox.Show("Patron service not available. Cannot search patrons.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Patrons.Clear();
            var result = _patronService.SearchPatrons(SearchKeyword);
            foreach (var p in result)
                Patrons.Add(p);
        }

        private void Add()
        {
            if (_patronService == null)
            {
                MessageBox.Show("Patron service not available. Cannot add patron.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Đảm bảo chỉ mở 1 dialog
                var dialog = new Views.PatronDialog();
                var viewModel = new PatronDialogViewModel();
                dialog.DataContext = viewModel;

                // Set owner để dialog không bị overlap
                var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                if (currentWindow != null)
                {
                    dialog.Owner = currentWindow;
                }

                // Set dialog properties
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                dialog.ShowInTaskbar = false;
                dialog.Topmost = true;

                var result = dialog.ShowDialog();
                if (result == true)
                {
                    var vm = dialog.DataContext as PatronDialogViewModel;
                    if (vm?.Patron != null)
                    {
                        _patronService.AddPatron(vm.Patron);
                        LoadPatrons();
                        MessageBox.Show($"Patron '{vm.Patron.FullName}' added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Add: {ex.Message}\n\nDetails: {ex.InnerException?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Edit()
        {
            if (SelectedPatron == null)
            {
                MessageBox.Show("Please select a patron to edit.", "No Patron Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_patronService == null)
            {
                MessageBox.Show("Patron service not available. Cannot edit patron.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Đảm bảo chỉ mở 1 dialog
                var dialog = new Views.PatronDialog();
                var viewModel = new PatronDialogViewModel(SelectedPatron);
                dialog.DataContext = viewModel;

                // Set owner để dialog không bị overlap
                var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                if (currentWindow != null)
                {
                    dialog.Owner = currentWindow;
                }

                // Set dialog properties
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                dialog.ShowInTaskbar = false;
                dialog.Topmost = true;

                var result = dialog.ShowDialog();
                if (result == true)
                {
                    var vm = dialog.DataContext as PatronDialogViewModel;
                    if (vm?.Patron != null)
                    {
                        _patronService.UpdatePatron(vm.Patron);
                        LoadPatrons();
                        MessageBox.Show($"Patron '{vm.Patron.FullName}' updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Edit: {ex.Message}\n\nDetails: {ex.InnerException?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            // ĐÃ SỬA LỖI: Sử dụng ActivatorUtilities.CreateInstance
            historyWindow!.DataContext = ActivatorUtilities.CreateInstance<PatronBorrowingHistoryViewModel>(
                (Application.Current as App)?.Services!, // ServiceProvider
                SelectedPatron.PatronId,
                _borrowingService,
                _bookService,
                _patronService,
                _fineService
            );

            historyWindow.Show();
        }
    }
}
