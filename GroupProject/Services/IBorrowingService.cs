using BusinessObject;

public interface IBorrowingService
{
    List<Borrowing> GetAllBorrowings();
    Borrowing? GetBorrowingById(int id);
    List<Borrowing> GetBorrowingsByPatron(int patronId);
    void BorrowBook(int bookId, int patronId, DateOnly borrowDate, DateOnly dueDate);
    void ReturnBook(int borrowingId);
    List<Borrowing> GetOverdueBorrowings();
    bool IsOverdue(Borrowing borrowing);
    decimal CalculateFine(Borrowing borrowing);

    void MarkBookCopyAsLost(int borrowingId);
    void MarkBookCopyAsDamaged(int borrowingId);
    void MarkBookCopyAsNormal(int borrowingId);
    void UpdateBorrowing(Borrowing borrowing); // <-- Thêm dòng này
}