using Library_Management_System.Data.Enum;
using Microsoft.AspNetCore.Identity;
namespace Library_Management_System.Data;

    public class Borrowing : BaseEntity
    {
        public string UserId {get; set; } = default!;
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public Book Book { get; set; } = default!;
        public IdentityUser User { get; set;} = default!;
        public string RequestMessage { get; set; } = default!;
        public Status Status { get; set; }
        public bool Returned { get; set; }
    }