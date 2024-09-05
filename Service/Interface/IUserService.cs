using Library_Management_System.Dto;
using Library_Management_System.Dto.User;

namespace Library_Management_System.Service.Interface
{
    public interface IUserService
    {
        Task<BaseResponse<bool>> UserRegistration(CreateUserRequestDto request);
        Task<BaseResponse<bool>> UserLogin(UserLoginRequestDto request);
        Task<BaseResponse<bool>> UserResetPassword(UserResetPasswordRequestDto request);
        Task<BaseResponse<bool>> UserChangePassword(UserChangePasswordRequestDto request);
        Task<BaseResponse<List<UserDto>>> GetUsers();
        Task<BaseResponse<UserDto>> GetUser(string Id);
        Task<BaseResponse<bool>> SignOutAsync();
    }
}
