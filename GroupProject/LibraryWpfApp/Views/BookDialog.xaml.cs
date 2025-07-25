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

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for BookDialog.xaml
    /// </summary>
    public partial class BookDialog : Window
    {
        public BookDialog()
        {
            InitializeComponent();

        }
        //private void Save_Click(object sender, RoutedEventArgs e)
        //{
        //    DialogResult = true;
        //    Close();
        //}

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BookDialogViewModel viewModel)
            {
                var imagePath = viewModel.Book.ImagePath;
                // Bạn có thể kiểm tra xem imagePath đã đổi chưa hoặc xử lý gì đó nếu cần

                // Gán DialogResult = true để đóng và trả lại dữ liệu
                this.DialogResult = true;
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
