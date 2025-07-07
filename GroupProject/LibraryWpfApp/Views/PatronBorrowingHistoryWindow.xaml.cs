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
using LibraryWpfApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for PatronBorrowingHistoryWindow.xaml
    /// </summary>

    public partial class PatronBorrowingHistoryWindow : Window
    {
        public PatronBorrowingHistoryWindow(int patronId) // Constructor khi mở cửa sổ với PatronId
        {
            InitializeComponent();

            // ĐÃ SỬA LỖI: Sử dụng ActivatorUtilities.CreateInstance
            this.DataContext = ActivatorUtilities.CreateInstance<PatronBorrowingHistoryViewModel>(
                (Application.Current as App)?.Services!, // Lấy ServiceProvider
                patronId,
                (Application.Current as App)?.Services.GetRequiredService<IBorrowingService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IBookService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IPatronService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IFineService>()!
            );
        }

        // Overload constructor không tham số nếu có nơi gọi không truyền ID
        public PatronBorrowingHistoryWindow()
        {
            InitializeComponent();

            // Nếu không có PatronId cụ thể, sử dụng CurrentUserID từ AppContext
            this.DataContext = ActivatorUtilities.CreateInstance<PatronBorrowingHistoryViewModel>(
                (Application.Current as App)?.Services!, // Lấy ServiceProvider
                AppContext.CurrentUserID,
                (Application.Current as App)?.Services.GetRequiredService<IBorrowingService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IBookService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IPatronService>()!,
                (Application.Current as App)?.Services.GetRequiredService<IFineService>()!
            );
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
