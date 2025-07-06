using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Borrowing
{
    public int BorrowingId { get; set; }

    public int? BookId { get; set; }

    public int? PatronId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public bool? IsReturned { get; set; }

    public string? Status { get; set; }

    public virtual Book? Book { get; set; }

    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    public virtual Patron? Patron { get; set; }
}
