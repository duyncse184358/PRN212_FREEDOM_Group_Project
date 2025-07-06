using BusinessObject;
using System.Collections.Generic;

namespace Services
{
    public interface IPatronService
    {
        List<Patron> GetAllPatrons();
        Patron? GetPatronById(int id);
        void AddPatron(Patron patron);
        void UpdatePatron(Patron patron);
        void DeletePatron(int id);
        List<Patron> SearchPatrons(string keyword);
    }
}