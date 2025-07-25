using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWpfApp.Helpers
{
    public static class AppSession
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }
        public static string CurrentUserRole { get; set; }

        // Nếu muốn: Thêm tiện ích kiểm tra
        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(CurrentUserName);

        public static void Reset()
        {
            CurrentUserId = 0;
            CurrentUserName = string.Empty;
            CurrentUserRole = string.Empty;
        }

        public static bool IsAdmin => CurrentUserRole == "Administrator";
        public static bool IsStaff => CurrentUserRole == "Staff";

    }
}
