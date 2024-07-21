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
}
