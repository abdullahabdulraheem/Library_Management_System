using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Dto.Book
{
    public class UpdateBookRequestDto
    {

        [Required(ErrorMessage = "Please title is required")]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Please author is required")]
        public required string Author { get; set; }
        [Required(ErrorMessage = "Please genre is required")]
        public required string Genre { get; set; }
        [Required(ErrorMessage = "Please isbn is required")]
        public required string ISBN { get; set; }
        [Required(ErrorMessage = "Please copies is required")]
        [Range(1, 100, ErrorMessage = "The minimum book is 1 ")]
        public int Copies { get; set; }
    }
}
