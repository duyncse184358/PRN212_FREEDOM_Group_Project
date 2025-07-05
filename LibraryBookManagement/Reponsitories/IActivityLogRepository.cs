using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IActivityLogRepository
    {
        List<ActivityLog> GetAll();
        void LogAction(int userId, string action);
    }
}
