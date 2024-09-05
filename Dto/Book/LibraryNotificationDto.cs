namespace Library_Management_System.Dto.Book
{
    public class LibraryNotificationDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}