using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LibraryWpfApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for FineManagementWindow.xaml
    /// </summary>
    public partial class FineManagementWindow : Window
    {
        public FineManagementWindow()
        {
            InitializeComponent();
            this.DataContext = (Application.Current as App)?.Services.GetRequiredService<FineManagementViewModel>();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid?.SelectedItem != null)
            {
                // Example: Show details of the selected fine
                // var selectedFine = dataGrid.SelectedItem as FineDisplayModel;
                // MessageBox.Show($"Fine ID: {selectedFine?.FineID}\nAmount: {selectedFine?.Amount:C}");
            }
        }
    }
}
