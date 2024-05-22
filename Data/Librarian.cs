namespace Library_Management_System.Data;

    public class Librarian
    {
        public int LibrarianID { get; set; }
        public int UserID { get; set; } 

        public User User { get; set; } = default!;
    }