using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObject;
using Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User? _currentUser;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_currentUser != null)
            {
                Title = $"Library Management System - Welcome {_currentUser.FullName}";
                TxtUserInfo.Text = $"Welcome, {_currentUser.FullName}";
                // Initialize main interface based on user role
                LoadMainInterface();
            }
        }

        private void LoadMainInterface()
        {
            // Load default view - Books Management
            BtnBooks_Click(null, null);
        }

        // Navigation Event Handlers
        private void BtnBooks_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Books UserControl
            TxtStatus.Text = "Books Management";
        }

        private void BtnPatrons_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Patrons UserControl
            TxtStatus.Text = "Patrons Management";
        }

        private void BtnBorrowing_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Borrowing UserControl
            TxtStatus.Text = "Borrowing Management";
        }

        private void BtnReturns_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Returns UserControl
            TxtStatus.Text = "Returns Management";
        }

        private void BtnFines_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Fines UserControl
            TxtStatus.Text = "Fines Management";
        }

        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Categories UserControl
            TxtStatus.Text = "Categories Management";
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Users UserControl
            TxtStatus.Text = "Users Management";
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load Reports UserControl
            TxtStatus.Text = "Reports";
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = _serviceProvider.GetRequiredService<LibraryWpfApp.Windows.LoginWindow>();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}