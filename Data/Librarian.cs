using Library_Management_System.Data;

namespace Library_Management_System.Models;

    public class Librarian : BaseEntity
    {
        public Guid LibrarianID { get; set; }
        public Guid UserID { get; set; } 

        public User User { get; set; } = default!;
    }