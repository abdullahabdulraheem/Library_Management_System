namespace Library_Management_System.Models;

    public class Book
    {
        public Guid BookID { get; set; }
        public string? Title { get; set; } 
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? ISBN { get; set; }
        public int Copies { get; set; }
        public bool Availability => Copies > 0;
    }