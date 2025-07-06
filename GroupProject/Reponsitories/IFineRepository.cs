using BusinessObject;
using System.Collections.Generic;

namespace Reponsitories
{
    public interface IFineRepository
    {
        List<Fine> GetAll();
        Fine? GetById(int id);
        List<Fine> GetByPatronId(int patronId);
        void Add(Fine f);
        void Update(Fine fine);
        void MarkAsPaid(int fineId);
    }
}