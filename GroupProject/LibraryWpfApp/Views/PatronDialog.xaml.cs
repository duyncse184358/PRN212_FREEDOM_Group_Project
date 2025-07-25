using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for PatronDialog.xaml
    /// </summary>
    public partial class PatronDialog : Window
    {
        public PatronDialog()
        {
            InitializeComponent();

            // Đảm bảo dialog hiển thị trên cùng
            this.Loaded += (s, e) =>
            {
                this.Activate();
                this.Focus();
            };
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate the data before saving
                var viewModel = DataContext as ViewModels.PatronDialogViewModel;
                if (viewModel == null)
                {
                    System.Windows.MessageBox.Show("ViewModel is null.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }

                if (viewModel.Patron == null)
                {
                    System.Windows.MessageBox.Show("Patron data is null.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }

                if (viewModel.IsValid())
                {
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error in Save_Click: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
