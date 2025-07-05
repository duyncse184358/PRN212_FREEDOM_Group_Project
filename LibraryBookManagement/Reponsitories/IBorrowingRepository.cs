using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IBorrowingRepository
    {
        List<Borrowing> GetAll();
        List<Borrowing> GetByPatronId(int patronId);
        void Add(Borrowing borrowing);
        void MarkReturned(int borrowingId);
    }
}
