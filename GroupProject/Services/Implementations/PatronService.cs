using BusinessObject;
using Reponsitories;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations
{
    public class PatronService : IPatronService
    {
        private readonly IPatronRepository _repo;

        public PatronService(IPatronRepository repo)
        {
            _repo = repo;
        }

        public List<Patron> GetAllPatrons() => _repo.GetAll();
        public Patron? GetPatronById(int id) => _repo.GetById(id);
        public void AddPatron(Patron patron) => _repo.Add(patron);
        public void UpdatePatron(Patron patron) => _repo.Update(patron);
        public void DeletePatron(int id) => _repo.Delete(id);
        public List<Patron> SearchPatrons(string keyword) => _repo.Search(keyword);
    }
}