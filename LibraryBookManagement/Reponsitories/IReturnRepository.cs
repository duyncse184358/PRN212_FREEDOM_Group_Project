using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IReturnRepository
    {
        List<Return> GetAll();
        Return GetByBorrowingId(int borrowingId);
        void Add(Return r);
    }

}
