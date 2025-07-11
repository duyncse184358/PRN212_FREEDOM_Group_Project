using System.Collections.Generic;
using BusinessObject;
using Repositories;

namespace Services
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repository;

        public BookCopyService(IBookCopyRepository repository)
        {
            _repository = repository;
        }

        public List<BookCopy> GetByBookId(int bookId) => _repository.GetByBookId(bookId);

        public BookCopy? GetById(int copyId) => _repository.GetById(copyId);

        public void UpdateStatus(int copyId, string newStatus) => _repository.UpdateStatus(copyId, newStatus);

        public void Add(BookCopy copy)
            => _repository.Add(copy);

        public void Delete(int copyId)
            => _repository.Delete(copyId);

        public void UpdateStatusByBookId(int bookId, string newStatus)
    => _repository.UpdateStatusByBookId(bookId, newStatus);


        public void AddCopies(int bookId, int number)
        {
            for (int i = 0; i < number; i++)
                _repository.Add(new BookCopy { BookId = bookId, Status = "Available" });
        }

    }
}