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
using LibraryWpfApp.Helpers;

namespace LibraryWpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string WelcomeMessage { get; set; }

        //public bool IsAdmin => AppContext.IsAdmin;
        public bool IsAdmin => AppSession.CurrentUserRole == "Administrator";
        public bool IsStaff => AppSession.CurrentUserRole == "Staff";

        public ICommand LogoutCommand { get; }
        public ICommand OpenBookManagementCommand { get; }
        public ICommand OpenPatronManagementCommand { get; }
        public ICommand OpenBorrowReturnCommand { get; }
        public ICommand OpenFineManagementCommand { get; }
        public ICommand OpenUserManagementCommand { get; }
        public ICommand OpenCategoryManagementCommand { get; }
        public ICommand OpenReportsCommand { get; }
        //public ICommand OpenProfileCommand { get; }
        public ICommand OpenOverdueBooksCommand { get; }
        public ICommand OpenMemberBookListCommand { get; } // Bổ sung
        public ICommand OpenMyBorrowingHistoryCommand { get; } // Bổ sung

        public MainViewModel()
        {
            WelcomeMessage = $"Welcome, {AppSession.CurrentUserName} ({AppSession.CurrentUserRole})";

            LogoutCommand = new RelayCommand(Logout);
            OpenBookManagementCommand = new RelayCommand(() => OpenWindow<Views.BookWindow>());
            OpenPatronManagementCommand = new RelayCommand(() => OpenWindow<Views.PatronWindow>());
            OpenBorrowReturnCommand = new RelayCommand(() => OpenWindow<Views.BorrowReturnWindow>());
            OpenFineManagementCommand = new RelayCommand(() => OpenWindow<Views.FineManagementWindow>());
            //OpenUserManagementCommand = new RelayCommand(() => OpenWindow<Views.UserManagementWindow>());
            OpenUserManagementCommand = new RelayCommand(OpenUserManagement);


            OpenCategoryManagementCommand = new RelayCommand(() => OpenWindow<Views.CategoryWindow>());
            OpenReportsCommand = new RelayCommand(() => OpenWindow<Views.ReportWindow>());
            OpenMemberBookListCommand = new RelayCommand(() => OpenWindow<Views.BookWindow>()); // Bổ sung

            OpenMyBorrowingHistoryCommand = new RelayCommand(OpenMyBorrowingHistory);
        }

        private T? GetWindow<T>() where T : Window
        {
            return (Application.Current as App)?.Services.GetRequiredService<T>();
        }

        private void OpenWindow<T>() where T : Window
        {
            // Kiểm tra xem window đã mở chưa
            var existingWindow = Application.Current.Windows.OfType<T>().FirstOrDefault();
            if (existingWindow != null)
            {
                // Nếu đã mở, focus vào window đó
                existingWindow.Activate();
                existingWindow.Focus();
                return;
            }

            // Nếu chưa mở, tạo window mới
            var window = GetWindow<T>();
            if (window != null)
            {
                window.Owner = Application.Current.MainWindow;
                window.Show();
            }
        }

        //private void Logout()
        //{
        //    foreach (Window window in Application.Current.Windows.OfType<Window>().ToList())
        //    {
        //        if (!(window is LoginWindow))
        //        {

        //            window.Close();
        //        }
        //    }

        //    //AppContext.Reset();
        //    AppSession.Reset();
        //    (Application.Current as App)?.Services.GetRequiredService<LoginWindow>()?.Show();
        //}

        private void Logout()
        {
            // Tạo loginWindow trước khi đóng các cửa sổ khác
            var loginWindow = (Application.Current as App)?.Services.GetRequiredService<LoginWindow>();

            // Nếu null thì không làm gì
            if (loginWindow == null) return;

            // Mở login trước
            loginWindow.Show();

            // Sau đó đóng các cửa sổ khác
            foreach (Window window in Application.Current.Windows.OfType<Window>().ToList())
            {
                if (!(window is LoginWindow))
                {
                    window.Close();
                }
            }

            // Reset session
            AppSession.Reset();
        }


        private void OpenUserManagement()
        {
            if (AppSession.CurrentUserRole?.Equals("Staff", StringComparison.OrdinalIgnoreCase) == true)
            {
                MessageBox.Show("You do not have permission to access this feature.",
                   "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            OpenWindow<Views.UserManagementWindow>();
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