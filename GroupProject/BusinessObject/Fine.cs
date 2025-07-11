using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Fine
{
    public int FineId { get; set; }

    public int? BorrowingId { get; set; }

    public decimal? FineAmount { get; set; }

    public bool? Paid { get; set; }

    public int? PatronId { get; set; }
    public DateTime? FineDueDate { get; set; }   // <-- THÊM DÒNG NÀY
    public string? FineType { get; set; }        // <-- THÊM DÒNG NÀY
    public int? LateDays { get; set; }           // <-- ĐÃ CÓ

    public DateTime? FineDate { get; set; }

    public virtual Borrowing? Borrowing { get; set; }

    public virtual Patron? Patron { get; set; }

}
