using Library_Management_System.Data.Enum;
using Microsoft.AspNetCore.Identity;
namespace Library_Management_System.Data.Entities;

public class Borrowing : BaseEntity
{
    public required string UserId { get; set; }
    public Guid BookId { get; set; } = default!;
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public Status Status { get; set; }
    public bool Returned { get; set; }

    public ICollection<Message> LibarianMessages { get; set; } = new HashSet<Message>();
}