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

namespace LibraryWpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string WelcomeMessage { get; set; }
        public bool IsAdmin => AppContext.IsAdmin;
        public bool IsLibrarian => AppContext.IsLibrarian;
        public bool IsStaff => AppContext.IsStaff;

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
        //    OpenProfileCommand = new RelayCommand(() => GetWindow<Views.ProfileWindow>()?.Show());
            OpenOverdueBooksCommand = new RelayCommand(() => GetWindow<Views.OverdueBooksWindow>()?.Show());
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
    }
}
