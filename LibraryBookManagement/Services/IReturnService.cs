using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Services
{
    public interface IReturnService
    {
        List<Return> GetAllReturns();
        void ProcessReturn(Return returnItem);
    }
}
