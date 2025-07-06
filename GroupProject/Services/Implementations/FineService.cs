using BusinessObject;
using Reponsitories;
using Services;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _repo;

        public FineService(IFineRepository repo)
        {
            _repo = repo;
        }

        public List<Fine> GetAllFines() => _repo.GetAll();
        public Fine? GetFineById(int id) => _repo.GetById(id);
        public List<Fine> GetFinesByPatron(int patronId) => _repo.GetByPatronId(patronId);
        public void AddFine(Fine fine) => _repo.Add(fine);
        public void UpdateFine(Fine fine) => _repo.Update(fine);
        public void PayFine(int fineId)
        {
            var fine = GetFineById(fineId);
            if (fine != null && (fine.Paid == null || fine.Paid == false))
            {
                fine.Paid = true;
                _repo.Update(fine);
            }
        }
    }
}