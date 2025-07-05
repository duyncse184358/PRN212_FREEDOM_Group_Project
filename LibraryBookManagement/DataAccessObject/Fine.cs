using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Fine
{
    public int FineId { get; set; }

    public int? BorrowingId { get; set; }

    public decimal? FineAmount { get; set; }

    public bool? Paid { get; set; }

    public virtual Borrowing? Borrowing { get; set; }
}
