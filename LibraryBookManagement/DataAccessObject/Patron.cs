using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Patron
{
    public int PatronId { get; set; }

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? MembershipType { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
