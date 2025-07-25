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

    public int? CategoryId { get; set; }

    public string? Status { get; set; }


    public decimal? Price { get; set; } // Thêm dòng này
    public string? ImagePath { get; set; } // Đường dẫn ảnh sách

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Category? Category { get; set; }
    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

}
