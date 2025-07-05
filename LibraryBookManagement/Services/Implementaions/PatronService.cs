using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class PatronService : IPatronService
    {
        private readonly IPatronRepository repository;
        public PatronService(IPatronRepository repository) => this.repository = repository;
        
        public List<Patron> GetAllPatrons() => repository.GetAll();
        
        public Patron GetPatronById(int id)
        {
            var patron = repository.GetById(id);
            if (patron == null) throw new Exception("Patron not found");
            return patron;
        }
        
        public void AddPatron(Patron patron)
        {
            if (string.IsNullOrWhiteSpace(patron.FullName)) throw new ArgumentException("Patron name is required");
            repository.Add(patron);
        }
        
        public void UpdatePatron(Patron patron) => repository.Update(patron);
        
        public void DeletePatron(int id) => repository.Delete(id);
    }
}
