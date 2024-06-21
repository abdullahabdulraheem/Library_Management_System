using Library_Management_System.Data;

namespace Library_Management_System.Models;

    public class Borrowing : BaseEntity
    {
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }
        // Borrow Status enum

        public Book Book { get; set; } = default!;
        public User User { get; set; } = default!;
        public Status status { get; set; }
    }