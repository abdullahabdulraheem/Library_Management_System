namespace Library_Management_System.Data.Entities
{
    public class LibarianMessage : BaseEntity
    {
        public Guid BorrowId { get; set; }
        public Borrowing Borrowing { get; set; }
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
    }
}
