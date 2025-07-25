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
using ClosedXML.Excel;
using LibraryWpfApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            this.DataContext = (Application.Current as App)?.Services.GetRequiredService<ReportViewModel>();
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = DataContext as ReportViewModel;
                if (viewModel != null)
                {
                    viewModel.GenerateReport();
                }
                else
                {
                    MessageBox.Show("Không thể kết nối với dữ liệu báo cáo!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not ReportViewModel vm || vm.BorrowingsReport.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = "LibraryReport.xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Report");

                    // Header
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Book Title";
                    worksheet.Cell(1, 3).Value = "Patron Name";
                    worksheet.Cell(1, 4).Value = "Borrow Date";
                    worksheet.Cell(1, 5).Value = "Due Date";
                    worksheet.Cell(1, 6).Value = "Return Date";
                    worksheet.Cell(1, 7).Value = "Status";
                    worksheet.Cell(1, 8).Value = "Fine Amount";
                    worksheet.Cell(1, 9).Value = "Fine Paid";

                    // Data rows
                    var row = 2;
                    foreach (var item in vm.BorrowingsReport)
                    {
                        worksheet.Cell(row, 1).Value = item.BorrowingID;
                        worksheet.Cell(row, 2).Value = item.BookTitle;
                        worksheet.Cell(row, 3).Value = item.PatronName;
                        worksheet.Cell(row, 4).Value = item.BorrowDate.ToString("dd/MM/yyyy");
                        worksheet.Cell(row, 5).Value = item.DueDate.ToString("dd/MM/yyyy");
                        worksheet.Cell(row, 6).Value = item.ReturnDate?.ToString("dd/MM/yyyy") ?? "";
                        worksheet.Cell(row, 7).Value = item.Status;
                        worksheet.Cell(row, 8).Value = item.FineAmount;
                        worksheet.Cell(row, 9).Value = item.IsFinePaid ? "Yes" : "No";
                        row++;
                    }

                    // Format
                    worksheet.Columns().AdjustToContents();
                    worksheet.SheetView.FreezeRows(1);

                    workbook.SaveAs(saveFileDialog.FileName);

                    MessageBox.Show("Export successful!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during export:\n{ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        }
}
