using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace LibraryWpfApp.ViewModels
{
    public class UserDialogViewModel : BaseViewModel
    {
        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Roles { get; set; } = new ObservableCollection<string>
        {
            //"Administrator", "Member"
            "Administrator","Staff"
        };

        public UserDialogViewModel()
        {
            User = new BusinessObject.User
            {
                UserId = 0,
                UserName = "",
                Password = "",
                FullName = "",
                Role = "Administrator"
            };
        }

        public UserDialogViewModel(User originalUser)
        {
            // Tạo một bản copy hoàn toàn mới để tránh Entity Framework tracking conflict
            User = new BusinessObject.User
            {
                UserId = originalUser.UserId,
                UserName = originalUser.UserName ?? "",
                Password = originalUser.Password ?? "",
                FullName = originalUser.FullName ?? "",
                Role = originalUser.Role ?? "Administrator"
            };
        }
    }
}
