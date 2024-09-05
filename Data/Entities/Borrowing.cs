using Library_Management_System.Data.Enum;
using Microsoft.AspNetCore.Identity;
namespace Library_Management_System.Data.Entities;

public class Borrowing : BaseEntity
{
    public string UserId { get; set; }
    public Guid BookId { get; set; } = default!;
    public DateTime BorrowDate { get; set; }
    public DateTime? DueDate { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public LendingStatus Status { get; set; }
    public bool Returned { get; set; }

    public ICollection<LibraryNotification> LibarianMessages { get; set; } = new HashSet<LibraryNotification>();
}