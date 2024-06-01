namespace Library_Management_System.Models;

    public class Librarian
    {
        public Guid LibrarianID { get; set; }
        public Guid UserID { get; set; } 

        public User User { get; set; } = default!;
    }