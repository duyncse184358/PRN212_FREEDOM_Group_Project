using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace LibraryWpfApp.ViewModels
{
    public class PatronDialogViewModel : BaseViewModel
    {
        private Patron _patron;

        public Patron Patron
        {
            get => _patron;
            set
            {
                _patron = value;
                OnPropertyChanged();
                // Notify all wrapper properties
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(Phone));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(MembershipType));
                OnPropertyChanged(nameof(RegistrationDate));
            }
        }

        // Wrapper properties for binding
        public string? FullName
        {
            get => _patron?.FullName;
            set
            {
                if (_patron != null && _patron.FullName != value)
                {
                    _patron.FullName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Address
        {
            get => _patron?.Address;
            set
            {
                if (_patron != null && _patron.Address != value)
                {
                    _patron.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Phone
        {
            get => _patron?.Phone;
            set
            {
                if (_patron != null && _patron.Phone != value)
                {
                    _patron.Phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Email
        {
            get => _patron?.Email;
            set
            {
                if (_patron != null && _patron.Email != value)
                {
                    _patron.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? MembershipType
        {
            get => _patron?.MembershipType;
            set
            {
                if (_patron != null && _patron.MembershipType != value)
                {
                    _patron.MembershipType = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? RegistrationDate
        {
            get => _patron?.RegistrationDate;
            set
            {
                if (_patron != null && _patron.RegistrationDate != value)
                {
                    _patron.RegistrationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> MembershipTypes { get; } = new ObservableCollection<string>
        {
            "Standard", "Premium", "Student", "Faculty", "Basic"
        };

        public PatronDialogViewModel()
        {
            Patron = new BusinessObject.Patron
            {
                PatronId = 0,
                FullName = "",
                Address = "",
                Phone = "",
                Email = "",
                MembershipType = "Standard",
                RegistrationDate = DateTime.Now
            };
        }

        public PatronDialogViewModel(Patron originalPatron)
        {
            // Tạo một bản copy hoàn toàn mới để tránh Entity Framework tracking conflict
            Patron = new BusinessObject.Patron
            {
                PatronId = originalPatron.PatronId,
                FullName = originalPatron.FullName ?? "",
                Address = originalPatron.Address ?? "",
                Phone = originalPatron.Phone ?? "",
                Email = originalPatron.Email ?? "",
                MembershipType = originalPatron.MembershipType ?? "Standard",
                RegistrationDate = originalPatron.RegistrationDate
            };
        }

        // Validation method that can be called before saving
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                System.Windows.MessageBox.Show("Full Name is required.", "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Phone))
            {
                System.Windows.MessageBox.Show("Phone Number is required.", "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(MembershipType))
            {
                System.Windows.MessageBox.Show("Membership Type is required.", "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
