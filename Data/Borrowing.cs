namespace Library_Management_System.Data;

    public class Borrowing
    {
        public int BorrowingID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }

        public Book Book { get; set; } = default!;
        public User User { get; set; } = default!;
    }