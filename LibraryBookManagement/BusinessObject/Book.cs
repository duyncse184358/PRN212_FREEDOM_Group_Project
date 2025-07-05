using System;
using System.Collections.Generic;

namespace BusinessObject;
public partial class Book
{
    public int BookId { get; set; }

    public string? Isbn { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public int? PublicationYear { get; set; }

    public string? Genre { get; set; }

    public int? NumberOfCopies { get; set; }

    public int? AvailableCopies { get; set; }

    public string? ShelfLocation { get; set; }

    public string? CoverImageUrl { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Category? Category { get; set; }
} 
