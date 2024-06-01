namespace Library_Management_System.Models;

    public class Borrowing
    {
        public Guid BorrowingID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }

        public Book Book { get; set; } = default!;
        public User User { get; set; } = default!;
    }