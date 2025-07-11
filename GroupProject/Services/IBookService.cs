using BusinessObject;
using System.Collections.Generic;
using System;

namespace Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        List<Book> SearchBooks(string keyword);
        void BorrowBook(Borrowing newBorrowing);
        void ReturnBook(int borrowingId);
        void MarkBookStatus(int bookId, string status);
        void AddBookWithCopies(Book book);
    }
}