using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Services
{
    public interface IPatronService
    {
        List<Patron> GetAllPatrons();
        Patron GetPatronById(int id);
        void AddPatron(Patron patron);
        void UpdatePatron(Patron patron);
        void DeletePatron(int id);
    }
}
