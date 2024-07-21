namespace Library_Management_System.Dto.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int Copies { get; set; }
        public bool Availability { get; set; }
    }
}