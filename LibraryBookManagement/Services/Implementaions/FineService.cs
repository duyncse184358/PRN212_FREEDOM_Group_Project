using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Reponsitories;

namespace Services.Implementaions
{
    public class FineService : IFineService
    {
        private readonly IFineRepository repository;
        public FineService(IFineRepository repository) => this.repository = repository;

        public List<Fine> GetFinesByPatron(int patronId) => repository.GetByPatronId(patronId);
        public void AddFine(Fine fine) => repository.Add(fine);
        public void PayFine(int fineId) => repository.MarkAsPaid(fineId);
    }
}
