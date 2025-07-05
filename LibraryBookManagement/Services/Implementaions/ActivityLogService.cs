using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository repository;
        public ActivityLogService(IActivityLogRepository repository) => this.repository = repository;

        public List<ActivityLog> GetAllLogs() => repository.GetAll();
        public void Log(int userId, string action) => repository.LogAction(userId, action);
    }
}
