using AspNetCoreHero.ToastNotification.Abstractions;
using Library_Management_System.Data.Context;
using Library_Management_System.Data.Entities;
using Library_Management_System.Dto;
using Library_Management_System.Dto.Book;
using Library_Management_System.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Service
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly INotyfService _notyf;

        public BookService(LibraryDbContext libraryDbContext , INotyfService notyf)
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


        public async Task<BaseResponse<bool>> UpdateBook(Guid Id, UpdateBookRequestDto request)
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
    }
}
