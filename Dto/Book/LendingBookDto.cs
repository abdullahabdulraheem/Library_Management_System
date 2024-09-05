using Library_Management_System.Data.Enum;

namespace Library_Management_System.Dto.Book
{
    public class LendingBookDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; } = default!;
        public DateTime BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? BookName { get; set; }
        public string? FullName { get; set; }
        public LendingStatus Status { get; set; }
        public bool Returned { get; set; }
    }
}
