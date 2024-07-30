namespace Library_Management_System.Data.Entities
{
    public class Message : BaseEntity
    {
        public Guid BorrowId { get; set; }
        public Borrowing Borrowing { get; set; }
        public string LibrarianMessageContent { get; set; }
        public string UserMessageContent { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
    }
}
