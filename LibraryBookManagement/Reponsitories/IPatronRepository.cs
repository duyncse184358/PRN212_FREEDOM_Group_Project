using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Reponsitories
{
    public interface IPatronRepository
    {
        List<Patron> GetAll();
        Patron GetById(int id);
        void Add(Patron patron);
        void Update(Patron patron);
        void Delete(int id);
    }
}
