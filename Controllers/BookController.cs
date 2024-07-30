using Library_Management_System.Dto.Book;
using Library_Management_System.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize]
    [Route("book")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("create-book")]
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost("create-book")]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookRequestDto request)
        {
            var result = await _bookService.CreateBook(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Books");
            }
            return RedirectToAction("CreateBook");
        }

        [HttpGet("edit-book/{id}")]
        public async Task<IActionResult> EditBook(Guid id)
        {
            var result = await _bookService.GetBookById(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Books");
        }

        [HttpPost("edit-book/{id}")]
        public async Task<IActionResult> EditBook(UpdateBookRequestDto request, Guid id)
        {
            var result = await _bookService.UpdateBook(id, request);

            if (result.IsSuccessful)
            {
              return  RedirectToAction("Books");
            }
            return RedirectToAction("EditBook", new { id = id });
        }



        [HttpGet("Book-detail/{id}")]
        public async Task<IActionResult> BookDetail(Guid id)
        {
            var result = await _bookService.GetBookById(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Books");
        }


        [HttpGet("books")]
        public async Task<IActionResult> Books()
        {
            var result = await _bookService.GetBooks();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await _bookService.DeleteBook(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Books");
            }

            return RedirectToAction("Books");
        }

        public async Task<IActionResult> BorrowBookRequest(Guid id)
        {
            var result = await _bookService.BorrowBookRequest(id);

            if(result.IsSuccessful)
            {
                return RedirectToAction("Books");
            }

            return RedirectToAction("Books");
        }
        
    }
}
