using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Services
{
    public interface IActivityLogService
    {
        List<ActivityLog> GetAllLogs();
        void Log(int userId, string action);
    }
}
