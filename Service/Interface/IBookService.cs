using Library_Management_System.Data.Enum;
using Library_Management_System.Dto;
using Library_Management_System.Dto.Book;

namespace Library_Management_System.Service.Interface
{
    public interface IBookService
    {
        Task<BaseResponse<bool>> CreateBook(CreateBookRequestDto requestD);
        Task<BaseResponse<BookDto>> GetBookById(Guid Id);
        Task<BaseResponse<List<BookDto>>> GetBooks();
        Task<BaseResponse<bool>> UpdateBook(Guid Id, UpdateBookRequestDto request);
        Task<BaseResponse<bool>> DeleteBook(Guid Id);
        Task<BaseResponse<bool>> BorrowBookRequest(Guid BookId, string userId);
        Task<BaseResponse<bool>> ApprovedBorrowBookRequest(Guid borrowingId, LendingStatus lendingStatus);
        Task<BaseResponse<List<LendingBookDto>>> GetLendingBooks();
        Task<BaseResponse<List<List<LibraryNotificationDto>>>> GetNotificationByUserIdSlim(string userId);
        Task<BaseResponse<List<LibraryNotificationDto>>> GetNotificationByUserId(string userId);
    }
}
