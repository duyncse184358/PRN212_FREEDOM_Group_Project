using BusinessObject;
using System.Collections.Generic;

namespace Reponsitories
{
    public interface IPatronRepository
    {
        List<Patron> GetAll();
        Patron? GetById(int id);
        void Add(Patron patron);
        void Update(Patron patron);
        void Delete(int id);
        List<Patron> Search(string keyword);
    }
}