namespace Library_Management_System.Models;
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }