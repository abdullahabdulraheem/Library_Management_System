namespace Library_Management_System.Data.Entities
{
    public class LibraryNotification : BaseEntity
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
