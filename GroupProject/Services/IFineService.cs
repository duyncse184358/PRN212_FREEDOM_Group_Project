using BusinessObject;
using System.Collections.Generic;

namespace Services
{
    public interface IFineService
    {
        List<Fine> GetAllFines();
        Fine? GetFineById(int id);
        List<Fine> GetFinesByPatron(int patronId);
        void AddFine(Fine fine);
        void UpdateFine(Fine fine);
        void PayFine(int fineId);
        void DeleteFine(int fineId);

        void UnpayFine(int fineId);
    }
}