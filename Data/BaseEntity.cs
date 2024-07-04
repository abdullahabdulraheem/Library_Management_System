namespace Library_Management_System.Data;

public abstract class BaseEntity
{
    public int Id {get; set; }
    public DateTime CreatedOn {get; set; }
    public DateTime UpdatedOn {get; set; }
    public DateTime BorrowedOn {get; set; }

    // public string RequestMessage { get; set; } = string.Empty;
}