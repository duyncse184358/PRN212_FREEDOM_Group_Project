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
        public Patron Patron { get; set; }
        public ObservableCollection<string> MembershipTypes { get; set; } = new ObservableCollection<string>
        {
            "Standard", "Premium", "Student", "Faculty", "Basic"
        };

        public PatronDialogViewModel()
        {
            Patron = new BusinessObject.Patron();
            Patron.MembershipType = MembershipTypes.FirstOrDefault() ?? "Standard";
            Patron.RegistrationDate = DateTime.Now;
        }

        public PatronDialogViewModel(Patron originalPatron)
        {
            Patron = new BusinessObject.Patron
            {
                PatronId = originalPatron.PatronId,
                FullName = originalPatron.FullName,
                Address = originalPatron.Address,
                Phone = originalPatron.Phone,
                Email = originalPatron.Email,
                MembershipType = originalPatron.MembershipType,
                RegistrationDate = originalPatron.RegistrationDate
            };
        }
    }
}
