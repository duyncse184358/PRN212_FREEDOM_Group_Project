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
using Microsoft.Extensions.DependencyInjection;
using Services;
using BusinessObject;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for CategoryDialog.xaml
    /// </summary>
    public partial class CategoryDialog : Window
    {
        private BusinessObject.Category _currentCategory; // Field lưu Category hiện tại
        private bool _isEditMode; // Phân biệt chế độ

        // Constructor cho chế độ thêm mới
        public CategoryDialog()
        {
            InitializeComponent();
            _isEditMode = false;
        }

        // Constructor cho chế độ chỉnh sửa
        public CategoryDialog(BusinessObject.Category categoryToEdit)
        {
            InitializeComponent();
            if (categoryToEdit != null)
            {
                _currentCategory = categoryToEdit;
                CategoryNameTextBox.Text = categoryToEdit.CategoryName;
                _isEditMode = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("Please enter a category name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_isEditMode && _currentCategory != null)
                {
                    // Chỉ cập nhật giá trị cho Category hiện tại
                    _currentCategory.CategoryName = categoryName;
                }
                else
                {
                    // Tạo mới Category và lưu vào property (nếu cần)
                    _currentCategory = new BusinessObject.Category
                    {
                        CategoryName = categoryName
                    };
                }
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
