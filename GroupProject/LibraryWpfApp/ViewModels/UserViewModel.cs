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
    public class UserViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public ObservableCollection<User> Users { get; set; } = new();
        public User? SelectedUser { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public UserViewModel(IUserService userService) // Removed IRoleService as Role is string now
        {
            _userService = userService;

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);

            LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            foreach (var u in _userService.GetAllUsers())
                Users.Add(u);
        }

        private void Add()
        {
            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.UserDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<UserDialogViewModel>();

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as UserDialogViewModel;
                if (vm != null)
                {
                    // No RoleId to set, Role is directly on User object
                    _userService.AddUser(vm.User);
                    LoadUsers();
                }
            }
        }

        private void Edit()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.UserDialog>();
            dialog!.DataContext = (Application.Current as App)?.Services.GetRequiredService<UserDialogViewModel>(
                new object[] { SelectedUser! });

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as UserDialogViewModel;
                if (vm != null)
                {
                    // No RoleId to set, Role is directly on User object
                    _userService.UpdateUser(vm.User);
                    LoadUsers();
                }
            }
        }

        private void Delete()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete user '{SelectedUser.UserName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _userService.DeleteUser(SelectedUser.UserId);
                LoadUsers();
            }
        }
    }
}
