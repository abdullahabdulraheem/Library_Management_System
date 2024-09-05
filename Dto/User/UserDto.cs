using Library_Management_System.Data;

namespace Library_Management_System.Dto.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
    }

    public class CreateUserRequestDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResetPasswordRequestDto
    {
        public string Email { get; set; }
    }

    public class UserChangePasswordRequestDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    // public class BorrowRequestDto
    // {
    //     public required string UserId { get; set; }
    //     public Guid BookId { get; set; } = default!;
    //     public DateTime BorrowDate { get; set; }
    //     public DateTime DueDate { get; set; }
    //     public Book Book { get; set; }
    //     public User User { get; set; }
    //     public Status Status { get; set; }
    //     public bool Returned { get; set; }
    // }
}
