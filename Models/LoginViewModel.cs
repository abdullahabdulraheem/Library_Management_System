using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required!")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; } = default!;
    }
}