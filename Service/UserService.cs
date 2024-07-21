using AspNetCoreHero.ToastNotification.Abstractions;
using Library_Management_System.Data;
using Library_Management_System.Data.Context;
using Library_Management_System.Data.Entities;
using Library_Management_System.Dto;
using Library_Management_System.Dto.Book;
using Library_Management_System.Dto.User;
using Library_Management_System.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_Management_System.Service
{
    public class UserService : IUserService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(LibraryDbContext libraryDbContext, UserManager<User> userManager,
            SignInManager<User> signInManager, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = libraryDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BaseResponse<UserDto>> GetUser(string Id)
        {
            try
            {
                var user = await _dbContext.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new BaseResponse<UserDto> { IsSuccessful = false, Message = "User not found" };
                }


                var result = new UserDto()
                {
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserType = user.UserType,
                    Id = user.Id

                };

                return new BaseResponse<UserDto> { IsSuccessful = true, Message = "retieved succesful", Data = result };

            }
            catch (Exception ex)
            {

                return new BaseResponse<UserDto> { IsSuccessful = false, Message = "Error : Updated Failed" };
            }
        }

        public async Task<BaseResponse<List<UserDto>>> GetUsers()
        {
            try
            {
                var books = await _dbContext.Users.Select(user => new UserDto
                {
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserType = user.UserType,
                    Id = user.Id

                }).ToListAsync();

                if (books.Count > 0)
                {
                    return new BaseResponse<List<UserDto>> { IsSuccessful = true, Message = "Retrived successfully", Data = books };
                }

                return new BaseResponse<List<UserDto>> { IsSuccessful = false, Message = "Failed" };

            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserDto>> { IsSuccessful = false, Message = "Error : Updated Failed" };
            }
        }

        public Task<BaseResponse<bool>> UserChangePassword(UserChangePasswordRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> UserLogin(UserLoginRequestDto request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Email);

                var result = await _signInManager
                    .PasswordSignInAsync(user!.UserName!, request.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                   
                    _notyfService.Success("You're Logged in successfully");
                    return new BaseResponse<bool> { IsSuccessful = true, Message = "You're Logged in successfully" };
                }
                _notyfService.Error("Invald Login Attempt");
                return new BaseResponse<bool> { IsSuccessful = true, Message = "Invald Login Attempt" };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Invald Login Attempt" };
            }
        }

        public async Task<BaseResponse<bool>> UserRegistration(CreateUserRequestDto request)
        {
            try
            {
                var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
                if (existingUser != null)
                {
                    _notyfService.Warning("User already exist!");
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "User already exist!" };
                }

                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    UserType = UserType.Librarian,
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    _notyfService.Error("User creation failed");
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "User creation failed" };
                }

                var addRole = await _userManager.AddToRoleAsync(user, "User");

                if(!addRole.Succeeded)
                {
                    _notyfService.Error("Add user role failed");
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "User creation failed" };
                }


                _notyfService.Success("Registration was successful");
                await _signInManager.SignInAsync(user, isPersistent: false);

                return new BaseResponse<bool> { IsSuccessful = true, Message = "Registration was successful." };

            }
            catch (Exception ex)
            {

                return new BaseResponse<bool> { IsSuccessful = false, Message = "An error occured" };
            }
        }

        public Task<BaseResponse<bool>> UserResetPassword(UserResetPasswordRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
