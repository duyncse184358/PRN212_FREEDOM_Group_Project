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
using System.ComponentModel;
using LibraryWpfApp.Helpers;

namespace LibraryWpfApp.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private User? _selectedUser;

        public ObservableCollection<User> Users { get; set; } = new();

        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        // Constructor mặc định (public parameterless constructor) cho XAML
        public UserViewModel()
        {
        }

        public UserViewModel(IUserService userService)
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

            // Khởi tạo ViewModel cho UserDialog (chế độ thêm mới)
            var viewModel = new UserDialogViewModel();
            dialog!.DataContext = viewModel;

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var vm = dialog.DataContext as UserDialogViewModel;
                    if (vm != null)
                    {
                        // Kiểm tra trùng UserName
                        var existingUser = _userService.GetAllUsers()
                                                       .FirstOrDefault(u => u.UserName == vm.User.UserName);

                        if (existingUser != null)
                        {
                            MessageBox.Show("Username already exists. Please choose a different one.",
                                            "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        _userService.AddUser(vm.User);
                        LoadUsers();
                        MessageBox.Show("User added successfully!",
                                        "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding the user: {ex.Message}",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }


        //private void Edit()
        //{
        //    if (SelectedUser == null)
        //    {
        //        MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.UserDialog>();
        //    // Tạo UserDialogViewModel với user hiện tại cho Edit
        //    dialog!.DataContext = new UserDialogViewModel(SelectedUser);

        //    if (dialog.ShowDialog() == true)
        //    {
        //        var vm = dialog.DataContext as UserDialogViewModel;
        //        if (vm != null)
        //        {
        //            _userService.UpdateUser(vm.User);
        //            LoadUsers();
        //        }
        //    }
        //}
        private void Edit()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = (Application.Current as App)?.Services.GetRequiredService<Views.UserDialog>();
            // Clone để tránh thay đổi trực tiếp
            var editingUser = new User
            {
                UserId = SelectedUser.UserId,
                UserName = SelectedUser.UserName,
                Password = SelectedUser.Password,
                FullName = SelectedUser.FullName,
                Role = SelectedUser.Role
            };

            dialog!.DataContext = new UserDialogViewModel(editingUser);

            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as UserDialogViewModel;
                if (vm != null)
                {
                    bool isUserNameChanged = vm.User.UserName != SelectedUser.UserName;
                    bool isUserNameExists = _userService.GetAllUsers()
                                                        .Any(u => u.UserName == vm.User.UserName && u.UserId != vm.User.UserId);

                    if (isUserNameChanged && isUserNameExists)
                    {
                        MessageBox.Show("Username already exists. Please choose a different one.",
                                        "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    _userService.UpdateUser(vm.User);
                    LoadUsers();
                    MessageBox.Show("User updated successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
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

            if (MessageBox.Show($"Are you sure you want to delete user '{SelectedUser.UserName}'?",
                                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // Lấy ID của user đang đăng nhập
                    int currentUserId = AppSession.CurrentUserId; // hoặc cách bạn đang lưu

                    _userService.DeleteUser(SelectedUser.UserId, currentUserId);
                    LoadUsers();
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
