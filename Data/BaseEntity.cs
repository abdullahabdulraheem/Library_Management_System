namespace Library_Management_System.Data;

public class BaseEntity
{
    public Guid Id {get; set; }
    public DateTime CreatedOn {get; set; }
    public DateTime? UpdatedOn {get; set; }
}