using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Library_Management_System.Data;
using Library_Management_System.Data.Enum;

namespace Library_Management_System.Models
{
    public class LibrarianRequestView
    {
        public string UserName {get; set;} = default!;
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<Book> Book { get; set; } = default!;
        public ICollection<IdentityUser> User { get; set;} = default!;
        public string RequestMessage { get; set; } = default!;
        public Status Status { get; set; }
    }
}