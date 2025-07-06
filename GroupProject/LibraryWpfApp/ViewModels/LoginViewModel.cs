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
        public string Password { get; set; } = ""; // This will be the plain text password from UI

        public ICommand LoginCommand { get; }

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(LoginUser);
        }
        public LoginViewModel()
        {
           
        }

        private void LoginUser()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Hash the plain text password from UI for comparison with PasswordHash in DB
            string hashedPassword = HashPassword(Password);

            var user = _userService.LoginUser(Username, hashedPassword); // Pass hashed password to service

            if (user != null)
            {
                AppContext.CurrentUserID = user.UserId;
                AppContext.CurrentUserName = user.Username;
               

                (Application.Current as App)?.Services.GetRequiredService<MainWindow>()?.Show();
                CloseLoginWindow();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseLoginWindow()
        {
            Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
        }

        // Simple SHA256 hashing function (for demo purposes only, use a stronger library in production)
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
