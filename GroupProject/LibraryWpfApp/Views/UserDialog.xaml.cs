using System.Windows;
using LibraryWpfApp.ViewModels;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for UserDialog.xaml
    /// </summary>
    public partial class UserDialog : Window
    {
        public UserDialog()
        {
            InitializeComponent();
            Loaded += UserDialog_Loaded;
        }

        private void UserDialog_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as UserDialogViewModel;
            if (vm != null && !string.IsNullOrEmpty(vm.User.Password))
            {
                PasswordBox.Password = vm.User.Password;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as UserDialogViewModel;
            if (vm == null) return;

            // Validation
            if (string.IsNullOrWhiteSpace(vm.User.UserName))
            {
                MessageBox.Show("Username is required!",
                              "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(vm.User.FullName))
            {
                MessageBox.Show("Full name is required!",
                              "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(vm.User.Role))
            {
                MessageBox.Show("Please select a role!",
                              "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Set password if provided
            if (!string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                vm.User.Password = PasswordBox.Password;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as UserDialogViewModel;
            if (vm != null)
            {
                vm.User.Password = PasswordBox.Password;
            }
        }
    }
}
