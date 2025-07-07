using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using LibraryWpfApp.Commands;
using Services;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;

        public ObservableCollection<Category> Categories { get; set; } = new();
        public Category? SelectedCategory { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public CategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            LoadCategories();
        }

        private void LoadCategories()
        {
            Categories.Clear();
            foreach (var c in _categoryService.GetAllCategories())
                Categories.Add(c);
        }

        private void Add()
        {
            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.CategoryDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<CategoryDialogViewModel>();

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as CategoryDialogViewModel;
                if (vm != null)
                {
                    _categoryService.AddCategory(vm.Category);
                    LoadCategories();
                }
            }
        }

        private void Edit()
        {
            if (SelectedCategory == null)
            {
                MessageBox.Show("Please select a category to edit.", "No Category Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.CategoryDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<CategoryDialogViewModel>(
                new object[] { SelectedCategory! });

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as CategoryDialogViewModel;
                if (vm != null)
                {
                    _categoryService.UpdateCategory(vm.Category);
                    LoadCategories();
                }
            }
        }

        private void Delete()
        {
            if (SelectedCategory == null)
            {
                MessageBox.Show("Please select a category to delete.", "No Category Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // You might need to check if any books are associated with this category before deleting.
            // For simplicity, let's assume direct deletion is allowed.
            if (MessageBox.Show($"Are you sure you want to delete category '{SelectedCategory.CategoryName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _categoryService.DeleteCategory(SelectedCategory.CategoryId);
                LoadCategories();
            }
        }
    }
}
