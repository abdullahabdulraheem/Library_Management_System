using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library_Management_System.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username {get; set;}  = default!;

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email {get; set;} = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;} = default!;

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password entered does not match!")]
        public string ConfirmPassword {get; set; } = default!;
    }
}