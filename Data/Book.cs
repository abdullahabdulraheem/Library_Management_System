namespace Library_Management_System.Data;

    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public int Copies { get; set; }
        public bool Availability { get; set; }
    }