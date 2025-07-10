using BusinessObject;

public class BookCopy
{
    public int CopyId { get; set; }
    public int BookId { get; set; }
    public string Status { get; set; }

    public virtual Book? Book { get; set; }
}
