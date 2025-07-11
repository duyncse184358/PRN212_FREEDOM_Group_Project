using BusinessObject;
using System.Collections.Generic;

namespace Reponsitories
{
    public interface IBorrowingRepository
    {
        List<Borrowing> GetAll();
        Borrowing? GetById(int id);
        List<Borrowing> GetByPatronId(int patronId);
        void Add(Borrowing borrowing);
        void Update(Borrowing borrowing);
        void MarkDamagedAndFine(int borrowingId);
        void MarkReturned(int borrowingId);
        void MarkLostAndFine(int borrowingId);
    }
}