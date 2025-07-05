using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace LibraryWpfApp.Windows
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public LoginWindow(IUserService userService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userService = userService;
            _serviceProvider = serviceProvider;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TxtUsername.Text.Trim();
                string password = TxtPassword.Password;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ShowError("Please enter both username and password.");
                    return;
                }

                // Hash the password (simple MD5 for demo - use better hashing in production)
                string passwordHash = HashPassword(password);

                // Attempt login
                var user = _userService.Login(username, passwordHash);

                if (user != null)
                {
                    // Login successful - open main window
                    var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                    mainWindow.SetCurrentUser(user);
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            TxtError.Text = message;
            TxtError.Visibility = Visibility.Visible;
        }

        private string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
