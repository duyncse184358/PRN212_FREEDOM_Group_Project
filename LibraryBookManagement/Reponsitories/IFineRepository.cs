using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IFineRepository
    {
        List<Fine> GetByPatronId(int patronId);
        void Add(Fine f);
        void MarkAsPaid(int fineId);
    }
}
