 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Commands;
using LibraryWpfApp.Views;
using Services;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public string Username { get; set; } = "";
        public string Password { get; set; } = ""; // Thuộc tính này sẽ được cập nhật từ PasswordBox_PasswordChanged

        public ICommand LoginCommand { get; }

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(LoginUser);
        }

        public LoginViewModel() : this((Application.Current as App)?.Services.GetRequiredService<IUserService>()!)
        {
            // Constructor này được gọi bởi XAML.
            // Nó ủy quyền việc khởi tạo thực tế cho constructor có tham số,
            // lấy IUserService từ DI container.
        }
        private void LoginUser()
        {
            Console.WriteLine("LoginUser method started."); // Debug: Ghi vào Output Window

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine("Login failed: Username or password is empty."); // Debug
                return;
            }

            Console.WriteLine($"Attempting login for Username: {Username}, Password: {Password}"); // Debug

            BusinessObject.User? user = null;
            try
            {
                // Gọi Service để kiểm tra đăng nhập
                user = _userService.LoginUser(Username, Password);
                Console.WriteLine($"_userService.LoginUser returned: {(user != null ? user.UserName : "null")}"); // Debug
            }
            catch (Exception ex)
            {
                // Ghi lỗi chi tiết vào Console và hiển thị MessageBox
                Console.WriteLine($"ERROR calling _userService.LoginUser: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                MessageBox.Show($"An error occurred during login: {ex.Message}\n\nCheck Output Window for details.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user != null)
            {
                AppContext.CurrentUserID = user.UserId;
                AppContext.CurrentUserName = user.UserName;
                AppContext.CurrentUserRole = user.Role ?? "Guest";

                Console.WriteLine($"Login successful! User: {AppContext.CurrentUserName}, Role: {AppContext.CurrentUserRole}"); // Debug

                // Mở MainWindow và đóng LoginWindow
                var mainWindow = (Application.Current as App)?.Services.GetRequiredService<Views.MainWindow>();
                if (mainWindow != null)
                {
                    mainWindow.Show();
                    Console.WriteLine("Main Window opened."); // Debug
                }
                else
                {
                    Console.WriteLine("Error: Main Window could not be resolved from DI."); // Debug
                }

                // Đóng tất cả các cửa sổ Login đang mở
                foreach (Window window in Application.Current.Windows.OfType<Views.LoginWindow>().ToList())
                {
                    window.Close();
                    Console.WriteLine("Login Window closed."); // Debug
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("Login failed: Invalid username or password."); // Debug
            }
        }
    }
}
