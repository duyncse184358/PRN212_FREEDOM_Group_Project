using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccessLayer.DAO;
using DataAccessLayer;

namespace Reponsitories.Implementations
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ActivityLogDAO dao;
        public ActivityLogRepository(LibraryDbContext context) => dao = new ActivityLogDAO(context);
        public List<ActivityLog> GetAll() => dao.GetAll();
        public void LogAction(int userId, string action) => dao.LogAction(userId, action);
    }
}
