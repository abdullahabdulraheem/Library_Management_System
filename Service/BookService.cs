using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Abstractions;
using Library_Management_System.Data.Context;
using Library_Management_System.Data.Entities;
using Library_Management_System.Data.Enum;
using Library_Management_System.Dto;
using Library_Management_System.Dto.Book;
using Library_Management_System.Service.Interface;
using Library_Management_System.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library_Management_System.Service
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly INotyfService _notyf;
        private readonly UserManager<User> _userManager;
        IHttpContextAccessor _httpContextAccessor;


        public BookService(LibraryDbContext libraryDbContext, INotyfService notyf, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = libraryDbContext;
            _notyf = notyf;
        }
        public async Task<BaseResponse<bool>> CreateBook(CreateBookRequestDto request)
        {

            try
            {
                var book = new Book()
                {
                    Author = request.Author,
                    Availability = request.Availability,
                    Copies = request.Copies,
                    CreatedOn = DateTime.Now,
                    Genre = request.Genre,
                    ISBN = request.ISBN,
                    Title = request.Title,
                    Id = Guid.NewGuid()
                };

                await _dbContext.Books.AddAsync(book);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _notyf.Success("Book Added Successfully");
                    return new BaseResponse<bool> { IsSuccessful = true, Message = "Book Added Successfully" };
                };
                _notyf.Error("Book Creation Failed");
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Book Creation Failed" };

            }
            catch (Exception ex)
            {
                _notyf.Error("Book Creation Failed");
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Error : Book Creation Failed" };
            }
        }


        public async Task<BaseResponse<bool>> UpdateBook(Guid Id, [FromForm] UpdateBookRequestDto request)
        {

            try
            {
                var book = await _dbContext.Books.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (book == null)
                {
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "Book not found" };
                }

                book.ISBN = request.ISBN;
                book.Title = request.Title;
                book.Genre = request.Genre;
                book.Title = request.Title;
                book.Author = request.Author;
                book.Copies = request.Copies;

                _dbContext.Books.Update(book);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _notyf.Success("Book Updated Successfully");
                    return new BaseResponse<bool> { IsSuccessful = true, Message = "Book Updated Successfully" };
                };
                _notyf.Error("Updated Failed");
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Updated Failed" };
            }
            catch (Exception ex)
            {
                _notyf.Error("Updated Failed");
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Error : Updated Failed" };
            }
        }


        public async Task<BaseResponse<BookDto>> GetBookById(Guid Id)
        {

            try
            {
                var book = await _dbContext.Books.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (book == null)
                {
                    return new BaseResponse<BookDto> { IsSuccessful = false, Message = "Book not found" };
                }


                var result = new BookDto()
                {
                    Author = book.Author,
                    Copies = book.Copies,
                    Availability = book.Availability,
                    Genre = book.Genre,
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,

                };

                return new BaseResponse<BookDto> { IsSuccessful = true, Message = "retieved succesful", Data = result };

            }
            catch (Exception ex)
            {

                return new BaseResponse<BookDto> { IsSuccessful = false, Message = "Error : Updated Failed" };
            }
        }


        public async Task<BaseResponse<List<BookDto>>> GetBooks()
        {

            try
            {
                var books = await _dbContext.Books.Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Availability = x.Availability,
                    Copies = x.Copies,
                    Genre = x.Genre,
                    ISBN = x.ISBN

                }).ToListAsync();

                if (books.Count > 0)
                {
                    return new BaseResponse<List<BookDto>> { IsSuccessful = true, Message = "Retrived successfully", Data = books };
                }

                return new BaseResponse<List<BookDto>> { IsSuccessful = false, Message = "Updated Failed" };

            }
            catch (Exception ex)
            {
                return new BaseResponse<List<BookDto>> { IsSuccessful = false, Message = "Error : Updated Failed" };
            }
        }


        public async Task<BaseResponse<bool>> DeleteBook(Guid Id)
        {

            try
            {
                var book = await _dbContext.Books.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (book == null)
                {
                    _notyf.Error("Book not found");
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "Book not found" };
                }

                _dbContext.Books.Remove(book);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _notyf.Error("Book Deleted Successfully");
                    return new BaseResponse<bool> { IsSuccessful = true, Message = "Book deleted Successfully" };
                };
                _notyf.Error("Delete Failed");
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Delete Failed" };

            }
            catch (Exception ex)
            {
                _notyf.Error("Delete Failed");
                return new BaseResponse<bool> { IsSuccessful = true, Message = "Error : Delete Failed" };
            }
        }

        public async Task<BaseResponse<bool>> BorrowBookRequest(Guid BookId, string userId)
        {
            try
            {
                var book = await _dbContext.Books.Where(x => x.Id == BookId).FirstOrDefaultAsync();

                var user = await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

                if (book is null)
                {
                    _notyf.Error("Book not found");
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "Error finding Book" };
                }

                if (user == null)
                {
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "User not found" };
                }

                var borrowing = new Borrowing()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    BookId = book.Id,
                    Status = LendingStatus.pending
                };

                await _dbContext.AddAsync(borrowing);


                var role = await _dbContext.Roles
                                .Where(x => x.Name == "Libarian")
                                .FirstOrDefaultAsync();

                if (role == null)
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "Libarian not found" };

                var userRoles = await _dbContext.UserRoles
                                .Where(x => x.RoleId == role.Id)
                                .FirstOrDefaultAsync();

                if (userRoles == null)
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "Libarian not found" };


                var message = new LibraryNotification()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    IsRead = false,
                    Message = $"{user!.FirstName} requested to borrow a copy of {book.Title} by {book.Author}.",
                    UserId = userRoles.UserId
                };

                await _dbContext.LibarianMessages.AddAsync(message);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _notyf.Success("Request Submitted!");
                    return new BaseResponse<bool> { IsSuccessful = true, Message = "Request Submitted" };
                }

                _notyf.Error("Unable to submit request", 8);
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Request Failed" };
            }
            catch (Exception ex)
            {
                _notyf.Error("Unable to Submit Request");
                return new BaseResponse<bool> { IsSuccessful = true, Message = "Request Failed" };
            }

        }


        public async Task<BaseResponse<bool>> ApprovedBorrowBookRequest(Guid borrowingId, LendingStatus lendingStatus)
        {
            try
            {

                var borrowing = await _dbContext.Borrowings
                                    .Where(x => x.Id == borrowingId)
                                    .FirstOrDefaultAsync();

                if (borrowing is null)
                {
                    _notyf.Error("Book not found");
                    return new BaseResponse<bool> { IsSuccessful = false, Message = "Error finding Book" };
                }

                if (borrowing.Status == lendingStatus)
                    return new BaseResponse<bool> { IsSuccessful = false, Message = $"Request has already been {lendingStatus.ToString()}" };


                borrowing.Status = lendingStatus;
                borrowing.UpdatedOn = DateTime.Now;
                borrowing.DueDate = DateTime.Now.AddDays(7);

                _dbContext.Borrowings.Update(borrowing);

                var message = new LibraryNotification()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    IsRead = false,
                    Message = $"Your request has been approved by the libarian you are to return the book on {borrowing.DueDate}",
                    UserId = borrowing.UserId,
                };

                await _dbContext.LibarianMessages.AddAsync(message);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _notyf.Success($"Request {lendingStatus.ToString()}!");
                    return new BaseResponse<bool> { IsSuccessful = true, Message = "Request Submitted" };
                }

                _notyf.Error("Unable to submit request", 8);
                return new BaseResponse<bool> { IsSuccessful = false, Message = "Request Failed" };
            }
            catch (Exception ex)
            {
                _notyf.Error("Unable to Submit Request");
                return new BaseResponse<bool> { IsSuccessful = true, Message = "Request Failed" };
            }

        }


        public async Task<BaseResponse<List<LendingBookDto>>> GetLendingBooks()
        {

            try
            {
                var lendingBooks = await _dbContext.Borrowings
                    .Include(x => x.Book)
                    .Include(x => x.User)
                    .Select(x => new LendingBookDto
                    {
                        Id = x.Id,
                        BookId = x.BookId,
                        BookName = x.Book.Title,
                        DueDate = x.DueDate,
                        BorrowDate = x.BorrowDate,
                        FullName = $"{x.User.FirstName} {x.User.LastName}",
                        Returned = x.Returned,
                        Status = x.Status,
                        UserId = x.UserId
                    }).ToListAsync();

                if (lendingBooks.Count > 0)
                {
                    return new BaseResponse<List<LendingBookDto>> { IsSuccessful = true, Message = "Retrived successfully", Data = lendingBooks };
                }

                return new BaseResponse<List<LendingBookDto>> { IsSuccessful = false, Message = "Updated Failed" };

            }
            catch (Exception ex)
            {
                return new BaseResponse<List<LendingBookDto>> { IsSuccessful = false, Message = "Error : Updated Failed" };
            }
        }
    }
}
