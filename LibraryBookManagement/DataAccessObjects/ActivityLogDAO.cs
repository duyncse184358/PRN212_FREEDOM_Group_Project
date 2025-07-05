using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class ActivityLogDAO
    {
        private readonly LibraryDbContext _context;

        public ActivityLogDAO(LibraryDbContext context) => _context = context;

        public List<ActivityLog> GetAll() => _context.ActivityLogs.ToList();

        public void LogAction(int userId, string action)
        {
            var log = new ActivityLog
            {
                UserId = userId,
                Action = action,
                LogDate = DateTime.Now
            };

            _context.ActivityLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
