namespace Library_Management_System.Dto.Book
{
    public class LibraryNotificationDto
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
    }
}