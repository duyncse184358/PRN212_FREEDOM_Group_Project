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
        public User User { get; set; }
        public ObservableCollection<string> Roles { get; set; } = new ObservableCollection<string>
        {
            "Administrator", "Librarian", "Staff"
        };

        public UserDialogViewModel()
        {
            User = new BusinessObject.User();
            User.Role = Roles.FirstOrDefault() ?? "Librarian";
        }

        public UserDialogViewModel(User originalUser)
        {
            User = new BusinessObject.User
            {
                UserId = originalUser.UserId,
                UserName = originalUser.UserName,
                Password = originalUser.Password,
                FullName = originalUser.FullName,
                Role = originalUser.Role
            };
        }
    }
}
