using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWpfApp
{
    public static class AppContext
    {
        public static int CurrentUserID { get; set; } = 0;
        public static string CurrentUserName { get; set; } = "";
        public static string CurrentUserRole { get; set; } = "";

        public static bool IsAdmin => CurrentUserRole.Equals("Administrator", System.StringComparison.OrdinalIgnoreCase);
        public static bool IsLibrarian => CurrentUserRole.Equals("Librarian", System.StringComparison.OrdinalIgnoreCase);
        public static bool IsStaff => CurrentUserRole.Equals("Staff", System.StringComparison.OrdinalIgnoreCase);


        public static void Reset()
        {
            CurrentUserID = 0;
            CurrentUserName = string.Empty;
            CurrentUserRole = string.Empty;
        }
    }
}
