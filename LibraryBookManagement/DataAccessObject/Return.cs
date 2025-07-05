using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Return
{
    public int ReturnId { get; set; }

    public int? BorrowingId { get; set; }

    public DateOnly ReturnDate { get; set; }

    public virtual Borrowing? Borrowing { get; set; }
}
