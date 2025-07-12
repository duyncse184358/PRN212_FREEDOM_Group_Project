using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Views;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace LibraryWpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string WelcomeMessage { get; set; }

        //public bool IsAdmin => AppContext.IsAdmin;
        public bool IsLibrarian => AppContext.IsLibrarian;

        public bool IsStaff => AppContext.IsStaff;
        public bool IsMember => AppContext.IsMember; // Bổ sung

        public ICommand LogoutCommand { get; }
        public ICommand OpenBookManagementCommand { get; }
        public ICommand OpenPatronManagementCommand { get; }
        public ICommand OpenBorrowReturnCommand { get; }
        public ICommand OpenFineManagementCommand { get; }
        public ICommand OpenUserManagementCommand { get; }
        public ICommand OpenCategoryManagementCommand { get; }
        public ICommand OpenReportsCommand { get; }
        public ICommand OpenProfileCommand { get; }
        public ICommand OpenOverdueBooksCommand { get; }
        public ICommand OpenMemberBookListCommand { get; } // Bổ sung
        public ICommand OpenMyBorrowingHistoryCommand { get; } // Bổ sung

        public MainViewModel()
        {
            WelcomeMessage = $"Welcome, {AppContext.CurrentUserName} ({AppContext.CurrentUserRole})";

            LogoutCommand = new RelayCommand(Logout);
            OpenBookManagementCommand = new RelayCommand(() => GetWindow<Views.BookWindow>()?.Show());
            OpenPatronManagementCommand = new RelayCommand(() => GetWindow<Views.PatronWindow>()?.Show());
            OpenBorrowReturnCommand = new RelayCommand(() => GetWindow<Views.BorrowReturnWindow>()?.Show());
            OpenFineManagementCommand = new RelayCommand(() => GetWindow<Views.FineManagementWindow>()?.Show());
            OpenUserManagementCommand = new RelayCommand(() => GetWindow<Views.UserManagementWindow>()?.Show());
            OpenCategoryManagementCommand = new RelayCommand(() => GetWindow<Views.CategoryWindow>()?.Show());
            OpenReportsCommand = new RelayCommand(() => GetWindow<Views.ReportWindow>()?.Show());
            OpenMemberBookListCommand = new RelayCommand(() => GetWindow<Views.BookWindow>()?.Show()); // Bổ sung

            OpenMyBorrowingHistoryCommand = new RelayCommand(OpenMyBorrowingHistory);
        }

        private T? GetWindow<T>() where T : Window
        {
            return (Application.Current as App)?.Services.GetRequiredService<T>();
        }

        private void Logout()
        {
            foreach (Window window in Application.Current.Windows.OfType<Window>().ToList())
            {
                if (!(window is LoginWindow))
                {
                    window.Close();
                }
            }

            AppContext.Reset();
            (Application.Current as App)?.Services.GetRequiredService<LoginWindow>()?.Show();
        }

        private void OpenMyBorrowingHistory()
        {
            int patronId = AppContext.CurrentPatronId;

            var app = Application.Current as App;
            var borrowingService = app.Services.GetRequiredService<IBorrowingService>();
            var bookService = app.Services.GetRequiredService<IBookService>();
            var patronService = app.Services.GetRequiredService<IPatronService>();
            var fineService = app.Services.GetRequiredService<IFineService>();

            var window = (Application.Current as App)?.Services.GetRequiredService<PatronBorrowingHistoryWindow>();
            window?.ShowDialog();
        }
    }
}