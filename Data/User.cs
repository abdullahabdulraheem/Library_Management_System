namespace Library_Management_System.Data;
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public UserType UserType { get; set; } = default!;
    }